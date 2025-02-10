package xbrlparser

import "encoding/xml"

type Concept struct {
	Name           string   `json:"name"`
	Label          string   `json:"label"`
	InferredFreq   string   `json:"inferred_freq"`
	AvailableUnits []string `json:"available_units"`
}

type Node struct {
	Label    string
	Href     string
	Children []*Node
	Matched  bool
	Concept  *Concept
}

type Linkbase struct {
	XMLName          xml.Name           `xml:"linkbase"`
	Xmlns            string             `xml:"xmlns,attr"`
	XmlnsLink        string             `xml:"xmlns:link,attr"`
	XmlnsXlink       string             `xml:"xmlns:xlink,attr"`
	PresentationLink []PresentationLink `xml:"presentationLink"`
}

type PresentationLink struct {
	Role string    `xml:"role,attr"`
	Loc  []Locator `xml:"loc"`
	Arcs []Arc     `xml:"presentationArc"`
}

type Locator struct {
	Href  string `xml:"href,attr"`
	Label string `xml:"label,attr"`
	Type  string `xml:"type,attr"`
}

type Arc struct {
	Order   string `xml:"order,attr"`
	Arcrole string `xml:"arcrole,attr"`
	From    string `xml:"from,attr"`
	To      string `xml:"to,attr"`
	Type    string `xml:"type,attr"`
}
