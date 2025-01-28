
Below is a detailed recalculation of Filing Scout AI’s per-query token cost under the Phi-4 model, along with a justification of how the blended token rate translates into real-world costs per query. The numbers show that while each 500-token AI query is cheap at the raw token level, other overhead (hosting, storage, indexing, etc.) justifies a higher retail price.

1. Blended Cost Per Token

Under the Phi-4 model:
	•	Input Token Price: $0.07 per 1 million tokens
	•	Output Token Price: $0.14 per 1 million tokens
	•	Ratio: 3:1 (on average, each query uses ~3 input tokens for every 1 output token)

1.1 Calculating the Blended Rate
	1.	Total Cost for 3 Million Input Tokens:
3 million × $0.07 per million = $0.21
	2.	Total Cost for 1 Million Output Tokens:
1 million × $0.14 per million = $0.14
	3.	Combined Cost for 4 Million Tokens:
$0.21 + $0.14 = $0.35
	4.	Cost per 1 Million Tokens (Blended):
$0.35 / 4 = $0.0875 per 1 million tokens

Hence the blended rate: $0.0875 per 1,000,000 tokens.

2. Cost Per Query

Because each AI query in Filing Scout AI consumes 500 tokens:
	1.	Cost per Single Token:
￼
	2.	Cost per 500-Token Query:
￼

Thus, serving one 500-token AI query costs Filing Scout AI about $0.000044 in raw token fees under the Phi-4 pricing model.

3. Illustrative Monthly Usage Costs

Even at scale, these token fees remain modest:

AI Queries/Month	Total Tokens	Raw Token Cost
5,000	2,500,000 (5,000×500)	(2,500,000 ÷ 1,000,000) × $0.0875 = $0.21875
50,000	25,000,000 (50,000×500)	(25,000,000 ÷ 1,000,000) × $0.0875 = $2.1875
500,000	250,000,000 (500,000×500)	(250,000,000 ÷ 1,000,000) × $0.0875 = $21.875
1,000,000	500,000,000 (1M×500)	(500,000,000 ÷ 1,000,000) × $0.0875 = $43.75

In other words:
	•	5,000 AI queries in a month cost ~$0.22 in raw token fees.
	•	1,000,000 AI queries in a month cost ~$43.75 in raw token fees.

4. Why the Retail Price is Higher

Despite the extremely low raw token cost, Filing Scout AI charges more per AI query (e.g., $0.03–$0.06/query) to account for additional operational overhead:
	1.	Hosting & Infrastructure
	•	Running servers for generating embeddings, storing indices, or caching data.
	2.	Engineering & Maintenance
	•	Ongoing development of advanced AI features, sentiment or risk-factor models, plus bug fixes.
	3.	Data Storage & Indexing
	•	Maintaining a comprehensive, easily searchable database of SEC filings.
	4.	Customer Support & Enhancements
	•	Staff costs for providing helpdesk support, building dashboards, usage analytics, etc.

These combined expenses vastly exceed the mere $0.000044 per query in token fees. As a result, user-facing prices need to be set meaningfully higher to sustain the service.

5. Example “Phi-4 AI Plan” Pricing

For illustration, here’s how a hypothetical AI plan might be structured, using the Phi-4 cost baseline but factoring in the true overhead:

Plan	Monthly Price	AI Query Allocation	Raw Token Cost	Overhead Costs	Margin
Basic AI	$150	200 queries/month	200 × $0.000044 = $0.0088	Est. $30–$40 server & staff cost	Remainder ￼
Professional AI	$500	600 queries/month	600 × $0.000044 = $0.0264	Est. $60–$80 overhead	Remainder ￼
Enterprise AI	$1,000	1,200 queries/month	1,200 × $0.000044 = $0.0528	Est. $100–$200 overhead	Remainder ￼

	Even though token fees are negligible, monthly overhead for hosting, indexing, feature development, plus profit margins drive the final subscription cost.

6. Conclusion
	1.	Token Fee is Tiny
	•	At just ~$0.000044 per 500-token query under the Phi-4 model, raw token costs are minimal.
	2.	Other Costs Dominate
	•	Infrastructure, engineering, and support overshadow the raw LLM token fees, justifying a higher retail per-query or subscription price.
	3.	Scalable & Sustainable
	•	Low token costs ensure economies of scale: as usage grows, raw token costs remain manageable, allowing Filing Scout AI to allocate more resources into user experience, advanced features, and support.
	4.	Transparent Pricing
	•	Showing how token-level costs compare to overhead clarifies why an apparent $0.000044 cost scales into monthly subscriptions of $100, $500, or more.

By blending the token pricing at $0.0875 per million tokens and consuming 500 tokens per query, Filing Scout AI can deliver advanced AI insights at low marginal cost—while still covering all the non-trivial operational costs that keep the platform robust, accurate, and user-friendly.