package xbrlparser

import (
	"bufio"
	"encoding/json"
	"encoding/xml"
	"os"
	"regexp"
	"sort"
	"strings"
	"unicode"
)

// LoadConcepts loads the concepts from a JSON file.
func LoadConcepts(filename string) (map[string]Concept, error) {
	file, err := os.Open(filename)
	if err != nil {
		return nil, err
	}
	defer file.Close()

	conceptMap := make(map[string]Concept)
	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()
		if line == "" {
			continue
		}

		var concept Concept
		if err := json.Unmarshal([]byte(line), &concept); err != nil {
			continue
		}
		if strings.Contains(concept.Label, "nil") {
			concept.Label = camelCaseToSpaced(concept.Name)
		}
		conceptMap[concept.Name] = concept
	}

	return conceptMap, scanner.Err()
}

// camelCaseToSpaced takes a camelCase or PascalCase string
// and inserts a space before each uppercase letter (except the first).
func camelCaseToSpaced(s string) string {
	// Regular expression to find positions before uppercase letters.
	re := regexp.MustCompile(`([A-Z][a-z]+)`)
	// Find all matches.
	matches := re.FindAllString(s, -1)
	// This approach works well when your string is in PascalCase.
	// However, if your string might have consecutive uppercase letters,
	// you might need a more advanced regular expression.
	return strings.TrimSpace(strings.Join(matches, " "))
}

// ParseXBRL parses an XBRL XML file using the provided concepts map and builds a tree structure.
func ParseXBRL(xmlPath string, concepts map[string]Concept) ([]*Node, error) {
	data, err := os.ReadFile(xmlPath)
	if err != nil {
		return nil, err
	}

	var lb Linkbase
	if err := xml.Unmarshal(data, &lb); err != nil {
		return nil, err
	}

	locators := make(map[string]Locator)
	relations := make(map[string][]string)

	for _, pl := range lb.PresentationLink {
		for _, l := range pl.Loc {
			locators[l.Label] = l
		}
		for _, arc := range pl.Arcs {
			if arc.Arcrole == "http://www.xbrl.org/2003/arcrole/parent-child" {
				relations[arc.From] = append(relations[arc.From], arc.To)
			}
		}
	}

	return buildTrees(locators, relations, concepts), nil
}

// buildTrees builds the tree structure from the locators and relationships.
func buildTrees(locators map[string]Locator, relations map[string][]string, concepts map[string]Concept) []*Node {
	childSet := make(map[string]bool)
	for _, children := range relations {
		for _, child := range children {
			childSet[child] = true
		}
	}

	var roots []*Node
	for label, loc := range locators {
		if !childSet[label] {
			cleanedLabel := cleanLabel(loc.Label)
			concept, matched := concepts[cleanedLabel]
			var conceptPtr *Concept
			if matched {
				conceptPtr = &concept
			}
			roots = append(roots, &Node{
				Label:   label,
				Href:    loc.Href,
				Matched: matched,
				Concept: conceptPtr,
			})
		}
	}

	sort.Slice(roots, func(i, j int) bool {
		return roots[i].Label < roots[j].Label
	})

	var buildTree func(label string) *Node
	buildTree = func(label string) *Node {
		loc, ok := locators[label]
		if !ok {
			return nil
		}

		cleanedLabel := cleanLabel(loc.Label)
		concept, matched := concepts[cleanedLabel]
		var conceptPtr *Concept
		if matched {
			conceptPtr = &concept
		}

		node := &Node{
			Label:   label,
			Href:    loc.Href,
			Matched: matched,
			Concept: conceptPtr,
		}

		if childrenLabels, found := relations[label]; found {
			sort.Strings(childrenLabels)
			for _, childLabel := range childrenLabels {
				childNode := buildTree(childLabel)
				if childNode != nil {
					node.Children = append(node.Children, childNode)
				}
			}
		}
		return node
	}

	var trees []*Node
	for _, root := range roots {
		trees = append(trees, buildTree(root.Label))
	}

	return trees
}

// cleanLabel removes the "loc_" prefix and, if present, the "us-gaap_" prefix.
func cleanLabel(label string) string {
	if strings.HasPrefix(label, "loc_") {
		name := strings.TrimPrefix(label, "loc_")
		if strings.HasPrefix(name, "us-gaap_") {
			return strings.TrimPrefix(name, "us-gaap_")
		}
		return name
	}
	return label
}

// ApplyGaapMapping recursively traverses the tree starting at 'node'. For each node,
// it always uses the node's Label (cleaned via cleanLabel) to check for "Abstract".
// If the cleaned key contains "Abstract", that substring is removed. Then the resulting key is
// used to look up the GAAP title in the provided mapping. If a mapping is found, the node's label
// is updated to ">>" plus the GAAP title. Otherwise, the cleaned key is formatted as a title.
func ApplyGaapMapping(node *Node, gaapMapping map[string]string) {
	if node == nil {
		return
	}

	// Always use the node's Label.
	key := strings.TrimSpace(node.Label)
	// Remove any locator prefixes.
	key = cleanLabel(key)

	if strings.Contains(key, "Abstract") {
		// Remove "Abstract" from the key.
		cleanedKey := strings.ReplaceAll(key, "Abstract", "")
		cleanedKey = strings.TrimSpace(cleanedKey)

		if newTitle, ok := gaapMapping[cleanedKey]; ok {
			node.Label = ">" + newTitle
		} else {
			// Format the cleaned key as a title (insert spaces etc.).
			node.Label = formatTitle(cleanedKey)
		}
	}

	// Recurse on children.
	for _, child := range node.Children {
		ApplyGaapMapping(child, gaapMapping)
	}
}

// formatTitle converts a CamelCase string into a title by inserting spaces before uppercase letters
// and then applying title case.
func formatTitle(s string) string {
	var result []rune
	for i, r := range s {
		if i > 0 && unicode.IsUpper(r) {
			// If the previous character is lower-case or the next character exists and is lower-case,
			// insert a space.
			prev := rune(s[i-1])
			var next rune
			if i+1 < len(s) {
				next = rune(s[i+1])
			}
			if unicode.IsLower(prev) || (i+1 < len(s) && unicode.IsLower(next)) {
				result = append(result, ' ')
			}
		}
		result = append(result, r)
	}
	// Convert the result to title case.
	return strings.Title(string(result))
}
