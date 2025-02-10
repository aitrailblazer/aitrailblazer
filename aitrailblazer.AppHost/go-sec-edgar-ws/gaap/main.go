package main

import (
	"bufio"
	"fmt"
	"os"
	"regexp"
	"sort"
	"strings"
)

func main() {
	// Input XSD file path.
	filePath := "./data/us-gaap-2024.xsd"
	// Output file paths.
	groupsOutputPath := "./data/extracted_gaap_groups.txt"
	mappingOutputPath := "./data/extracted_gaap_mapping.txt"
	groupsSummaryOutputPath := "./data/extracted_gaap_groups_summary.txt"

	// Open the XSD file.
	file, err := os.Open(filePath)
	if err != nil {
		fmt.Println("Error opening file:", err)
		return
	}
	defer file.Close()

	// Regular expressions:
	// reAbstract matches abstract elements (lines with abstract="true") and extracts the name.
	reAbstract := regexp.MustCompile(`<xs:element\s+[^>]*abstract="true"[^>]*name="([A-Za-z0-9_]+)"`)
	// reElement matches any xs:element line that includes a name attribute.
	reElement := regexp.MustCompile(`<xs:element\s+[^>]*name="([A-Za-z0-9_]+)"`)

	// groups maps an abstract group name to a slice of concrete element names.
	groups := make(map[string][]string)

	// Use "ungrouped" as the default group for elements that appear before any abstract element.
	currentGroup := "ungrouped"
	groups[currentGroup] = []string{}

	// Read the file line by line.
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := scanner.Text()

		// If the line defines an abstract element, update the current group.
		if reAbstract.MatchString(line) {
			matches := reAbstract.FindStringSubmatch(line)
			if len(matches) > 1 {
				currentGroup = matches[1]
				// Initialize the group if it does not already exist.
				if _, ok := groups[currentGroup]; !ok {
					groups[currentGroup] = []string{}
				}
				// Skip adding the abstract element itself as a concrete element.
				continue
			}
		}

		// Process concrete elements: only consider <xs:element> lines that do not contain abstract="true".
		if strings.Contains(line, "<xs:element") && !strings.Contains(line, `abstract="true"`) {
			matches := reElement.FindStringSubmatch(line)
			if len(matches) > 1 {
				elementName := matches[1]
				groups[currentGroup] = append(groups[currentGroup], elementName)
			}
		}
	}

	if err := scanner.Err(); err != nil {
		fmt.Println("Error reading file:", err)
		return
	}

	// Create a mapping from concrete element to abstract group.
	concreteToGroup := make(map[string]string)
	for group, elems := range groups {
		for _, elem := range elems {
			concreteToGroup[elem] = group
		}
	}

	// ----- Write the grouped output file -----
	// Only include groups that have one or more concrete elements.
	var groupNames []string
	for group, elems := range groups {
		if len(elems) > 0 {
			groupNames = append(groupNames, group)
		}
	}
	sort.Strings(groupNames)

	var groupsBuilder strings.Builder
	for _, group := range groupNames {
		groupsBuilder.WriteString(group + ":\n")
		elems := groups[group]
		sort.Strings(elems)
		for _, elem := range elems {
			groupsBuilder.WriteString("  " + elem + "\n")
		}
		groupsBuilder.WriteString("\n")
	}
	groupCount := len(groupNames)
	groupsBuilder.WriteString(fmt.Sprintf("Total groups with elements: %d\n", groupCount))

	err = os.WriteFile(groupsOutputPath, []byte(groupsBuilder.String()), 0644)
	if err != nil {
		fmt.Println("Error writing groups file:", err)
	} else {
		fmt.Println("Grouped financial concepts saved to", groupsOutputPath)
		fmt.Printf("Total groups with elements: %d\n", groupCount)
	}

	// ----- Write the key-value mapping output file -----
	var concreteNames []string
	for name := range concreteToGroup {
		concreteNames = append(concreteNames, name)
	}
	sort.Strings(concreteNames)

	var mappingBuilder strings.Builder
	for _, name := range concreteNames {
		line := fmt.Sprintf("%s -> %s\n", name, concreteToGroup[name])
		mappingBuilder.WriteString(line)
	}

	err = os.WriteFile(mappingOutputPath, []byte(mappingBuilder.String()), 0644)
	if err != nil {
		fmt.Println("Error writing mapping file:", err)
	} else {
		fmt.Println("Key-value mapping saved to", mappingOutputPath)
	}

	// ----- Write the groups summary file (group name and count of elements) -----
	var summaryBuilder strings.Builder
	for _, group := range groupNames {
		count := len(groups[group])
		summaryBuilder.WriteString(fmt.Sprintf("%s: %d\n", group, count))
	}

	err = os.WriteFile(groupsSummaryOutputPath, []byte(summaryBuilder.String()), 0644)
	if err != nil {
		fmt.Println("Error writing groups summary file:", err)
	} else {
		fmt.Println("Groups summary saved to", groupsSummaryOutputPath)
	}
}
