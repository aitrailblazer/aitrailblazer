package xbrlparser

import (
	"fmt"
	"strings"
)

func PrintTree(n *Node, level int) {
	cleanedLabel := cleanLabel(n.Label)
	// Skip nodes that are exactly "Statement Of Financial Position".
	if cleanedLabel == "Statement Of Financial Position" || cleanedLabel == "Income Statement" || cleanedLabel == "Statement Of Income And Comprehensive Income" || cleanedLabel == "Statement Of Stockholders Equity" || cleanedLabel == "Statement Of Partners Capital" || cleanedLabel == "Statement Of Cash Flows" || cleanedLabel == "Additional Cash Flow Elements And Supplemental Cash Flow Information" || cleanedLabel == "Supplemental Cash Flow Elements" || cleanedLabel == "Statement Of Cash Flows" || cleanedLabel == "Operating Cash Flows Direct Method" {
		// Process its children at the same level so that the skipped node is not printed.
		for _, child := range n.Children {
			PrintTree(child, level)
		}
		return
	}

	indent := strings.Repeat(" ", level)
	hasMatchedChildren := false

	// Determine whether to print this node:
	// 1. If it is matched and has a concept, or
	// 2. If any descendant is matched.
	if n.Matched && n.Concept != nil {
		hasMatchedChildren = true
	} else {
		for _, child := range n.Children {
			if hasMatchedDescendant(child) {
				hasMatchedChildren = true
				break
			}
		}
	}

	if hasMatchedChildren {
		// If the node is matched and has a concept, print its details including the description.
		if n.Matched && n.Concept != nil {
			// You can decide on the formatting here. For example, include description if non-empty.
			if n.Concept.Description != "" {
				fmt.Printf("%s- [L] %s | %s | %s\n",
					indent, cleanedLabel, n.Concept.Label, n.Concept.Description)
			} else {
				fmt.Printf("%s- [L] %s | %s\n",
					indent, cleanedLabel, n.Concept.Label)
			}
		} else {
			fmt.Printf("%s- %s\n", indent, cleanedLabel)
		}

		// Continue printing children at the next level.
		for _, child := range n.Children {
			PrintTree(child, level+1)
		}
	}
}

func hasMatchedDescendant(n *Node) bool {
	if n.Matched && n.Concept != nil {
		return true
	}
	for _, child := range n.Children {
		if hasMatchedDescendant(child) {
			return true
		}
	}
	return false
}
