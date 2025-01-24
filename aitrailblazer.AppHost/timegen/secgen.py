import os
import pandas as pd
from nixtla import NixtlaClient
import logging
import plotly.express as px

# Set up logging
logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')
logger = logging.getLogger(__name__)

# Access environment variables directly
api_key = os.getenv("TIMEGEN_KEY")
base_url = os.getenv("TIMEGEN_ENDPOINT")

# Validate environment variables
if not api_key:
    logger.error("TIMEGEN_KEY is not set. Please export it as an environment variable using `export TIMEGEN_KEY=your_api_key`.")
    raise EnvironmentError("Missing TIMEGEN_KEY environment variable.")

if not base_url:
    logger.error("TIMEGEN_ENDPOINT is not set. Please export it as an environment variable using `export TIMEGEN_ENDPOINT=your_base_url`.")
    raise EnvironmentError("Missing TIMEGEN_ENDPOINT environment variable.")

logger.info("Environment variables loaded successfully.")

# Initialize the Nixtla client
try:
    nixtla_client = NixtlaClient(api_key=api_key, base_url=base_url)
    logger.info("Nixtla client initialized successfully.")
except Exception as e:
    logger.exception("Failed to initialize Nixtla client.")
    raise e

# Define local file path for input
local_file = "TSLA_Revenues_Test.csv"

# Step 1: Read the local CSV file
try:
    df = pd.read_csv(local_file, parse_dates=True, dayfirst=True)
    logger.info(f"Local data loaded successfully from '{local_file}'.")
except FileNotFoundError:
    logger.error(f"File not found: {local_file}")
    raise
except Exception as e:
    logger.exception("Failed to load local data.")
    raise e

# Step 2: Dynamically detect column names and derive the target concept
try:
    # Detect columns for date, value, and others
    column_names = df.columns.tolist()
    logger.info(f"Detected columns in the input CSV: {column_names}")

    # Identify column names
    start_date_col = [col for col in column_names if "Start Date" in col][0]
    end_date_col = [col for col in column_names if "End Date" in col][0]
    concept_col = [col for col in column_names if "Concept" in col][0]
    value_col = [col for col in column_names if "Value" in col][0]

    logger.info(f"Identified columns - Start Date: {start_date_col}, End Date: {end_date_col}, Concept: {concept_col}, Value: {value_col}")

    # Extract unique concepts (e.g., Revenues, Assets)
    unique_concepts = df[concept_col].unique().tolist()
    logger.info(f"Unique concepts found: {unique_concepts}")

    # Select the first concept (assuming one concept per file) and filter data
    target_concept = unique_concepts[0]  # Replace this with user input or a specific selection logic if needed
    logger.info(f"Target concept selected: {target_concept}")

    df = df[df[concept_col] == target_concept]

    # Parse date columns
    df[start_date_col] = pd.to_datetime(df[start_date_col])
    df[end_date_col] = pd.to_datetime(df[end_date_col])

    # Ensure timestamps are sorted and check for duplicates
    df = df.sort_values(by=end_date_col).drop_duplicates(subset=[end_date_col])

    # Infer the frequency and validate the timestamps
    df.set_index(end_date_col, inplace=True)
    inferred_freq = pd.infer_freq(df.index)
    logger.info(f"Inferred frequency: {inferred_freq}")

    if inferred_freq is None:
        raise ValueError("Could not infer the frequency of the time column. Please check your data.")

    df.reset_index(inplace=True)
except Exception as e:
    logger.exception("Failed to process the column names or validate the time column.")
    raise e

# Step 3: Forecast based on the loaded data
try:
    forecast_df = nixtla_client.forecast(
        df=df,
        h=12,
        time_col=end_date_col,
        target_col=value_col,
        freq=inferred_freq
    )
    logger.info("Forecasting completed successfully.")
except Exception as e:
    logger.exception("Forecasting failed.")
    raise e

# Step 4: Generate Plotly plot
try:
    # Combine historical and forecasted data
    df["Type"] = "Historical"
    forecast_df["Type"] = "Forecast"

    # Rename forecasted time column for consistency
    forecast_df.rename(columns={"timestamp": end_date_col}, inplace=True)

    # Combine data for plotting
    combined_df = pd.concat(
        [
            df[[end_date_col, value_col, "Type"]].rename(columns={value_col: target_concept}),
            forecast_df[[end_date_col, "TimeGPT", "Type"]].rename(columns={"TimeGPT": target_concept}),
        ],
        ignore_index=True
    )

    # Create the plot with enhancements
    fig = px.line(
        combined_df,
        x=end_date_col,
        y=target_concept,
        color="Type",
        title=f"Tesla: {target_concept} Over Time",
        labels={end_date_col: "Quarter End", target_concept: "Value (USD)", "Type": "Data Type"},
        template="plotly_white"
    )

    # Customize line and markers
    fig.update_traces(
        mode="lines+markers",
        line=dict(width=2),
        marker=dict(size=6)
    )

    # Remove gridlines and set background color
    fig.update_layout(
        plot_bgcolor="white",
        xaxis=dict(showgrid=False, linecolor="black"),
        yaxis=dict(showgrid=False, linecolor="black")
    )

    # Add hover information
    fig.update_traces(
        hovertemplate="Quarter End: %{x}<br>Value: %{y}<extra></extra>"
    )

    # Save the plot as an HTML file
    script_directory = os.getcwd()
    plot_file = os.path.join(script_directory, f"forecast_tesla_{target_concept.lower()}_plot.html")
    fig.write_html(plot_file)

    logger.info(f"Forecast plot saved to '{plot_file}'.")
except Exception as e:
    logger.exception("Plotting failed.")
    raise e