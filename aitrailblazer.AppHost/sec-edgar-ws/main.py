# main.py
import os
import re
import requests
import logging
from io import BytesIO
import httpx
from io import BytesIO
import logging
from starlette.applications import Starlette
from starlette.requests import Request

from starlette.responses import JSONResponse, StreamingResponse, PlainTextResponse, HTMLResponse
from httpx import AsyncClient, RequestError
from starlette.routing import Route
import plotly.express as px
import csv
from io import StringIO
from sec_data.company_info import CompanyInfo
from sec_data.filings import SECFilings
from tenacity import retry, stop_after_attempt, wait_exponential
import urllib.parse
import io
import json
import logging
import pandas as pd
import plotly.graph_objects as go
from nixtla import NixtlaClient

from pydantic import BaseModel, Field
from typing import List, Optional, Any, Dict
import pandas as pd
import plotly.express as px

from pydantic import BaseModel, Field
from typing import Any, Dict
import pandas as pd
import plotly.express as px

from starlette.applications import Starlette
from starlette.requests import Request
from starlette.routing import Route
from starlette.responses import JSONResponse
from starlette.exceptions import HTTPException



import numpy as np
import math
import logging
from starlette.responses import JSONResponse

# Configure logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

# Configure Matplotlib
mpl_config_dir = "/tmp/matplotlib-config"
os.environ["MPLCONFIGDIR"] = mpl_config_dir

# Ensure the directory exists and is writable
if not os.path.exists(mpl_config_dir):
    os.makedirs(mpl_config_dir, exist_ok=True)
    logger.info(f"Created Matplotlib config directory: {mpl_config_dir}")
else:
    logger.info(f"Matplotlib config directory already exists: {mpl_config_dir}")

# Paths and setup
dataset_path = "data/company_tickers_exchange.json"
email = "FinanceDataCorp your-email@example.com"

# Initialize components
company_info = CompanyInfo(dataset_path)
sec_filings = SECFilings(email)

# Retry logic for fetching SEC filings
@retry(stop=stop_after_attempt(5), wait=wait_exponential(multiplier=1, min=2, max=10))
async def fetch_filing_with_retry(client, url):
    """Fetch filing content with retry logic."""
    response = await client.get(url)
    response.raise_for_status()
    return response.text

async def get_cik(request):
    """Endpoint to fetch the CIK for a given company ticker."""
    ticker = request.path_params["ticker"]
    try:
        if not re.match(r"^[A-Za-z0-9]+$", ticker):
            raise ValueError("Invalid ticker format. Only alphanumeric characters are allowed.")

        # Fetch the CIK
        cik = company_info.get_cik_by_ticker(ticker)

        # Ensure the CIK is returned as a string
        return PlainTextResponse(str(cik))

    except ValueError as e:
        logger.error(f"Error fetching CIK for ticker {ticker}: {e}")
        return PlainTextResponse(f"Error: {str(e)}", status_code=400)
    except Exception as e:
        logger.error(f"Unexpected error fetching CIK for ticker {ticker}: {e}")
        return PlainTextResponse(f"Server Error: {str(e)}", status_code=500)

import time
import re
import logging
from starlette.responses import PlainTextResponse

logger = logging.getLogger(__name__)

async def get_name(request):
    """
    Endpoint to fetch the Company Name for a given company ticker, with execution time measurement in milliseconds.
    """
    start_time = time.time()  # Start the timer
    
    ticker = request.path_params["ticker"]
    try:
        # Validate the ticker format
        if not re.match(r"^[A-Za-z0-9]+$", ticker):
            raise ValueError("Invalid ticker format. Only alphanumeric characters are allowed.")
        
        # Fetch the company name
        name = company_info.get_name_by_ticker(ticker)
        
        # Calculate execution time in milliseconds
        duration_ms = (time.time() - start_time) * 1000
        logger.info(f"Fetching Company Name for ticker {ticker} took {duration_ms:.3f} ms")
        
        # Return the company name as plain text
        return PlainTextResponse(name)
    
    except ValueError as e:
        duration_ms = (time.time() - start_time) * 1000
        logger.error(f"Error fetching Company Name for ticker {ticker} (Took {duration_ms:.3f} ms): {e}")
        return PlainTextResponse(f"Error: {str(e)}", status_code=400)
    
    except Exception as e:
        duration_ms = (time.time() - start_time) * 1000
        logger.error(f"Unexpected error fetching Company Name for ticker {ticker} (Took {duration_ms:.3f} ms): {e}")
        return PlainTextResponse(f"Error: An unexpected error occurred: {str(e)}", status_code=500)
    
async def get_exchange(request):
    """
    Endpoint to fetch the exchange for a given company ticker.
    """
    ticker = request.path_params["ticker"]
    try:
        # Validate the ticker format
        if not re.match(r"^[A-Za-z0-9]+$", ticker):
            raise ValueError("Invalid ticker format. Only alphanumeric characters are allowed.")
        
        # Fetch the exchange
        exchange = company_info.get_exchange_by_ticker(ticker)
        
        # Return the exchange as plain text
        return PlainTextResponse(exchange)
    
    except ValueError as e:
        logger.error(f"Error fetching exchange for ticker {ticker}: {e}")
        return PlainTextResponse(f"Error: {str(e)}", status_code=400)
    except Exception as e:
        logger.error(f"Unexpected error fetching exchange for ticker {ticker}: {e}")
        return PlainTextResponse(f"Error: An unexpected error occurred: {str(e)}", status_code=500)


async def get_all_exchanges(request: Request):
    """
    Endpoint to fetch all unique exchanges from the dataset.

    Args:
        request (Request): The HTTP request.

    Returns:
        JSONResponse: A response containing the list of unique exchanges.
    """
    try:
        # Fetch all unique exchanges
        exchanges = company_info.get_all_exchanges()
        return JSONResponse({"exchanges": exchanges})
    
    except Exception as e:
        logger.error(f"Unexpected error fetching exchanges: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)


async def tickers_by_exchange(request: Request):
    """
    Endpoint to fetch a list of companies (with CIK, name, and ticker) listed on a specific exchange.

    Args:
        request (Request): The HTTP request containing the path parameter.

    Returns:
        JSONResponse: A response containing a list of companies or an error message.
    """
    exchange = request.path_params.get("exchange")
    try:
        # Validate exchange input
        if not exchange:
            raise ValueError("Exchange name is required.")
        
        # Fetch companies for the specified exchange
        companies = company_info.get_companies_by_exchange(exchange)
        
        if not companies:
            return JSONResponse({"exchange": exchange, "companies": []}, status_code=404)
        
        # Return the list of companies
        return JSONResponse({"exchange": exchange, "companies": companies})
    
    except ValueError as e:
        logger.error(f"Error fetching companies for exchange {exchange}: {e}")
        return JSONResponse({"error": str(e)}, status_code=400)
    except Exception as e:
        logger.error(f"Unexpected error fetching companies for exchange {exchange}: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)



logger = logging.getLogger(__name__)

async def get_filings(request):
    """Endpoint to fetch filing history for a given company ticker, with execution time measurement."""
    
    start_time = time.time()  # Start the timer
    
    ticker = request.path_params["ticker"]
    try:
        # Validate ticker format
        if not re.match(r"^[A-Za-z0-9]+$", ticker):
            raise ValueError("Invalid ticker format. Only alphanumeric characters are allowed.")
        
        # Fetch CIK and filings
        cik = company_info.get_cik_by_ticker(ticker)
        filings = sec_filings.get_company_filings(cik)
        filings_df = sec_filings.filings_to_dataframe(filings)
        
        # Calculate execution time in milliseconds
        duration_ms = (time.time() - start_time) * 1000
        logger.info(f"Fetching filings for ticker {ticker} took {duration_ms:.3f} ms")
        
        return JSONResponse(filings_df.to_dict(orient="records"))
    
    except ValueError as e:
        duration_ms = (time.time() - start_time) * 1000
        logger.error(f"Error fetching filings for ticker {ticker} (Took {duration_ms:.3f} ms): {e}")
        return JSONResponse({"error": str(e)}, status_code=400)
    
    except Exception as e:
        duration_ms = (time.time() - start_time) * 1000
        logger.error(f"Unexpected error fetching filings for ticker {ticker} (Took {duration_ms:.3f} ms): {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)
    

async def get_available_forms(request):
    """
    Endpoint to fetch all unique forms available for a given company ticker.
    """

    start_time = time.time()  # Start the timer

    ticker = request.path_params["ticker"]
    try:
        # Validate ticker format
        if not re.match(r"^[A-Za-z0-9]+$", ticker):
            raise ValueError("Invalid ticker format. Only alphanumeric characters are allowed.")

        # Get the CIK and filings data
        cik = company_info.get_cik_by_ticker(ticker)
        filings = sec_filings.get_company_filings(cik)
        filings_df = sec_filings.filings_to_dataframe(filings)

        # Fetch all unique forms
        unique_forms = filings_df["form"].unique().tolist()

       # Calculate execution time in milliseconds
        duration_ms = (time.time() - start_time) * 1000
        logger.info(f"Fetching filings for ticker {ticker} took {duration_ms:.3f} ms")

        # Return the unique forms
        return JSONResponse({"ticker": ticker, "forms": unique_forms})

    except ValueError as e:
        logger.error(f"Error fetching forms for ticker {ticker}: {e}")
        return JSONResponse({"error": str(e)}, status_code=400)
    except Exception as e:
        logger.error(f"Unexpected error fetching forms for ticker {ticker}: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)

async def download_latest_filing_pdf(request):
    """
    Web service endpoint to download the latest filing of a specified form type as a PDF.
    """
    ticker = request.path_params["ticker"]
    form_type = request.path_params["form_type"]

    try:
        # Validate the ticker format
        if not re.match(r"^[A-Za-z0-9]+$", ticker):
            raise ValueError("Invalid ticker format. Only alphanumeric characters are allowed.")

        # Fetch available forms for the ticker
        cik = company_info.get_cik_by_ticker(ticker)
        filings = sec_filings.get_company_filings(cik)
        filings_df = sec_filings.filings_to_dataframe(filings)
        available_forms = filings_df["form"].unique().tolist()

        # Check if the requested form exists
        if form_type not in available_forms:
            raise ValueError(f"The requested form '{form_type}' does not exist for ticker {ticker}. Available forms: {available_forms}")

        # Fetch the HTML content and base URL for the requested form
        html_content, base_url = await download_latest_filing_html_python(ticker, form_type)

        # Convert HTML to PDF
        pdf_buffer = convert_html_to_pdf(html_content, base_url=base_url)

        # Return the PDF as a streaming response
        headers = {"Content-Disposition": f"attachment; filename={ticker}_latest_{form_type}.pdf"}
        return StreamingResponse(pdf_buffer, media_type="application/pdf", headers=headers)

    except ValueError as ve:
        logger.error(f"Value error for ticker {ticker}: {ve}")
        return JSONResponse({"error": str(ve)}, status_code=400)
    except IndexError:
        logger.error(f"No {form_type} filings found for ticker {ticker}")
        return JSONResponse({"error": f"No {form_type} filings found for ticker: {ticker}"}, status_code=404)
    except Exception as e:
        logger.error(f"Unexpected error for ticker {ticker}: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)


                          
async def get_filing_url(ticker: str, form_type: str):
    """
    Endpoint to get the URL of the filing for a given form type and company ticker.

    Args:
        ticker (str): The company ticker symbol.
        form_type (str): The form type (e.g., 10-K, 10-Q).

    Returns:
        PlainTextResponse: A plain-text response containing the filing URL.
    """
    try:
        # Validate the ticker and form type format
        if not re.match(r"^[A-Za-z0-9]+$", ticker):
            return PlainTextResponse("Invalid ticker format. Only alphanumeric characters are allowed.", status_code=400)

        if not re.match(r"^[A-Za-z0-9/ -]+$", form_type):
            return PlainTextResponse("Invalid form type format. Only alphanumeric characters, dashes (-), slashes (/), and spaces are allowed.", status_code=400)

        # Get the company's CIK
        cik = company_info.get_cik_by_ticker(ticker)
        if not cik:
            return PlainTextResponse(f"CIK not found for ticker: {ticker}", status_code=404)

        # Fetch filing data
        filings = sec_filings.get_company_filings(cik)
        filings_df = sec_filings.filings_to_dataframe(filings)

        # Check if filings are available for the given form type
        if filings_df.empty or form_type not in filings_df["form"].values:
            return PlainTextResponse(f"No {form_type} filings found for ticker: {ticker}", status_code=404)

        # Get the latest filing details for the specified form type
        latest_filing = filings_df[filings_df["form"] == form_type].iloc[0]
        accession_number = latest_filing["accessionNumber"].replace("-", "")
        primary_document = latest_filing["primaryDocument"]

        # Build the SEC filing base URL and full filing URL
        base_url = f"https://www.sec.gov/Archives/edgar/data/{cik}/{accession_number}/"
        filing_url = f"{base_url}{primary_document}"

        # Return the filing URL as plain text
        return PlainTextResponse(filing_url)

    except IndexError:
        # Handle no filings found for the specified form type
        logger.error(f"No {form_type} filings found for ticker: {ticker}")
        return PlainTextResponse(f"No {form_type} filings found for ticker: {ticker}", status_code=404)

    except ValueError as ve:
        # Handle validation errors
        logger.error(f"Validation error for ticker {ticker}: {ve}")
        return PlainTextResponse(str(ve), status_code=400)

    except Exception as e:
        # Handle unexpected errors
        logger.error(f"Unexpected error for ticker {ticker}: {e}")
        return PlainTextResponse(f"An unexpected error occurred: {str(e)}", status_code=500)
    
async def download_latest_filing_html(request):
    """
    Web service endpoint to download the latest filing of a specified form type as raw HTML with fixed image links.
    """
    try:
        # Parse the JSON body for required parameters
        body = await request.json()
        ticker = body.get("ticker")
        form_type = body.get("form_type")

        # Validate the inputs
        if not ticker or not re.match(r"^[A-Za-z0-9.-]+$", ticker):
            raise ValueError("Invalid or missing ticker. Only alphanumeric characters, dashes (-), and periods (.) are allowed.")
        if not form_type or not re.match(r"^[A-Za-z0-9/ -]+$", form_type):
            raise ValueError("Invalid or missing form type. Only alphanumeric characters, dashes (-), slashes (/), and spaces are allowed.")

        logger.info(f"Processing request for ticker: {ticker}, form_type: {form_type}")

        # Fetch available forms for the ticker
        cik = company_info.get_cik_by_ticker(ticker)
        filings = sec_filings.get_company_filings(cik)
        filings_df = sec_filings.filings_to_dataframe(filings)
        available_forms = filings_df["form"].unique().tolist()

        # Check if the requested form exists
        if form_type not in available_forms:
            raise ValueError(f"The requested form '{form_type}' does not exist for ticker {ticker}. Available forms: {available_forms}")

        # Fetch the HTML content and base URL for the requested form
        html_content, base_url = await download_latest_filing_html_python(ticker, form_type)
        logger.info(f"Base URL provided: {base_url}")

        # Return the modified HTML content as a streaming response
        headers = {"Content-Disposition": f"attachment; filename={ticker}_latest_{form_type}.html"}
        return StreamingResponse(BytesIO(html_content.encode("utf-8")), media_type="text/html", headers=headers)

    except ValueError as ve:
        logger.error(f"Value error: {ve}")
        return JSONResponse({"error": str(ve)}, status_code=400)
    except IndexError:
        logger.error(f"No {form_type} filings found for ticker {ticker}")
        return JSONResponse({"error": f"No {form_type} filings found for ticker: {ticker}"}, status_code=404)
    except Exception as e:
        logger.error(f"Unexpected error: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)


async def download_latest_filing_html_python(ticker: str, form_type: str) -> tuple[str, str]:
    """
    Fetch the latest filing of a given form type for a company ticker as raw HTML with fixed image links.
    
    Args:
        ticker (str): The company ticker symbol.
        form_type (str): The type of SEC form to fetch (e.g., "10-K", "10-Q").
    
    Returns:
        tuple: (html_content, base_url)
    """
    try:
        # Validate the ticker and form type format
        if not re.match(r"^[A-Za-z0-9]+$", ticker):
            raise ValueError("Invalid ticker format. Only alphanumeric characters are allowed.")
        if not re.match(r"^[A-Za-z0-9/ -]+$", form_type):
            raise ValueError("Invalid form type format. Only alphanumeric characters, dashes (-), slashes (/), and spaces are allowed.")

        # Get the company's CIK
        cik = company_info.get_cik_by_ticker(ticker)
        if not cik:
            raise ValueError(f"CIK not found for ticker: {ticker}")

        # Fetch filing data
        filings = sec_filings.get_company_filings(cik)
        filings_df = sec_filings.filings_to_dataframe(filings)

        if filings_df.empty or form_type not in filings_df["form"].values:
            raise IndexError(f"No {form_type} filings found for ticker: {ticker}")

        # Get the latest filing details for the specified form type
        latest_filing = filings_df[filings_df["form"] == form_type].iloc[0]
        accession_number = latest_filing["accessionNumber"].replace("-", "")
        file_name = latest_filing["primaryDocument"]

        # Build the SEC filing base URL and full filing URL
        base_url = f"https://www.sec.gov/Archives/edgar/data/{cik}/{accession_number}/"
        filing_url = f"{base_url}{file_name}"

        print(filing_url)

        # Fetch the filing content
        async with AsyncClient(timeout=120, headers={"User-Agent": email}) as client:
            try:
                html_content = await fetch_filing_with_retry(client, filing_url)
            except RequestError as e:
                logger.error(f"Failed to fetch filing for {ticker}: {e}")
                raise Exception(f"Failed to fetch filing from SEC for {ticker}.")

        # Replace relative image links with absolute URLs
        html_content = re.sub(
            r'(?<=<img src=")([^":]+)',  # Match the src attribute of <img> tags that do not contain a full URL
            lambda match: f"{base_url}{match.group(1)}",  # Replace with the full URL
            html_content,
        )

        # Return the modified HTML content and base URL
        return html_content, base_url

    except IndexError:
        logger.error(f"No {form_type} filings found for ticker {ticker}")
        raise IndexError(f"No {form_type} filings found for ticker: {ticker}")
    except ValueError as ve:
        logger.error(f"Value error for ticker {ticker}: {ve}")
        raise
    except Exception as e:
        logger.error(f"Unexpected error for ticker {ticker}: {e}")
        raise


async def get_xbrl_data(request):
    """
    Endpoint to fetch and process XBRL data for a given company ticker.
    """
    ticker = request.path_params["ticker"]
    concept = request.query_params.get("concept", "AssetsCurrent")
    unit = request.query_params.get("unit", "USD")

    try:
        # Validate ticker format
        if not re.match(r"^[A-Za-z0-9]+$", ticker):
            raise ValueError("Invalid ticker format. Only alphanumeric characters are allowed.")

        # Get CIK for the ticker
        cik = company_info.get_cik_by_ticker(ticker)
        if not cik:
            raise ValueError(f"CIK not found for ticker: {ticker}")

        # Fetch and process XBRL data
        xbrl_data = sec_filings.get_xbrl_data(cik)
        xbrl_df = sec_filings.process_xbrl_data(xbrl_data, concept=concept, unit=unit)

        # Convert DataFrame to JSON for response
        return JSONResponse(xbrl_df.to_dict(orient="records"))
    except ValueError as ve:
        logger.error(f"Value error for ticker {ticker}: {ve}")
        return JSONResponse({"error": str(ve)}, status_code=400)
    except Exception as e:
        logger.error(f"Unexpected error for ticker {ticker}: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)


async def forecast_xbrl_data_as_csv(request):
    """
    Endpoint to fetch XBRL data, forecast it using Nixtla, and return the forecasted data as CSV string.
    """
    ticker = request.path_params["ticker"]
    concept = request.query_params.get("concept", "AssetsCurrent")
    unit = request.query_params.get("unit", "USD")
    horizon = int(request.query_params.get("h", 12))  # Forecast horizon

    try:
        logger.info(f"Starting forecast for ticker: {ticker}, concept: {concept}, unit: {unit}, horizon: {horizon}")


        # Extract TimegenEndpoint and TimegenKey from the headers
        timegen_endpoint = request.headers.get("X-Timegen-Endpoint")
        timegen_key = request.headers.get("Authorization")

        # Validate that the required headers are present
        if not timegen_endpoint or not timegen_key:
            raise ValueError("Missing required headers: X-Timegen-Endpoint or Authorization")

        logger.info(f"Using TimegenEndpoint: {timegen_endpoint}")

        # Initialize Nixtla client
        nixtla_client = NixtlaClient(api_key=timegen_key, base_url=timegen_endpoint)
        
        # Fetch and process XBRL data
        xbrl_df = await fetch_xbrl_csv(ticker, concept, unit)

        # Ensure timestamps are sorted and infer frequency
        xbrl_df = xbrl_df.sort_values(by="enddate").drop_duplicates(subset=["enddate"])
        xbrl_df.set_index("enddate", inplace=True)
        inferred_freq = pd.infer_freq(xbrl_df.index)

        if not inferred_freq:
            raise ValueError("Could not infer frequency from the XBRL data.")

        # Reset index for Nixtla compatibility
        # xbrl_df.reset_index(inplace=True)

        # Forecast using Nixtla
        forecast_df = nixtla_client.forecast(
            df=xbrl_df,
            h=horizon,
            time_col="enddate",
            target_col="value",
            freq=inferred_freq
        )

        logger.info(f"Forecasting completed for ticker: {ticker}")

        # Convert forecasted data to CSV string
        csv_output = forecast_df.to_csv(index=False)

        # Return CSV as plain text response
        return PlainTextResponse(content=csv_output, media_type="text/csv")

    except ValueError as ve:
        logger.error(f"Value error: {ve}")
        return JSONResponse({"error": str(ve)}, status_code=400)
    except Exception as e:
        logger.error(f"Unexpected error during forecasting for ticker {ticker}: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)

# Assume fetch_xbrl_csv and clean_and_infer_frequency are defined elsewhere.

def sanitize_value(x, threshold=1e+308):
    """If x is a float and either not finite or its absolute value exceeds threshold, return None."""
    if isinstance(x, float):
        if not math.isfinite(x) or abs(x) > threshold:
            return None
    return x

def sanitize_df(df, threshold=1e+308):
    """Apply sanitize_value elementwise."""
    return df.applymap(lambda x: sanitize_value(x, threshold))

def replace_nan_in_dict(record):
    """Replace any float NaN in a dict with None."""
    for key, value in record.items():
        if isinstance(value, float) and np.isnan(value):
            record[key] = None
    return record

def convert_dates(df):
    """Convert 'enddate' column to datetime and format as 'YYYY-MM-DD' (dropping any time component)."""
    df["enddate"] = pd.to_datetime(df["enddate"], errors='coerce').dt.strftime("%Y-%m-%d")
    return df

def split_title(title: str, threshold: int = 50) -> str:
    """Insert an HTML line break (<br>) in the title if its length exceeds the threshold."""
    if len(title) > threshold:
        split_index = title.find(" ", threshold)
        if split_index == -1:
            split_index = threshold
        return title[:split_index] + "<br>" + title[split_index+1:]
    return title

async def forecast_xbrl_data_plot(request: Request):
    """
    Endpoint to fetch XBRL data, optionally forecast it using Nixtla, and return a JSON object containing:
      - original_data_json: JSON representation of the original (historical) data,
      - forecast_data_json: JSON representation of the forecast data (empty if forecast is skipped),
      - combined_data_json: JSON representation of the combined historical and forecast data,
      - combined_data_csv: CSV string generated from the combined DataFrame,
      - combined_plot_html: an HTML snippet of a Plotly chart combining historical and (if available) forecast data,
      - forecast_plot_html: an HTML snippet of a forecast-only Plotly chart (empty if forecast is skipped).
    
    The JSON body should include:
      - "concept": concept identifier,
      - "label": a label for the chart,
      - "unit": (optional, default "USD"),
      - "h": (optional forecast horizon; default 12),
      - "data": a list of data records,
      - "inferred_freq": the inferred frequency,
      - "name": a concept identifier,
      - "forecast": (optional boolean; default true; if false, forecasting is skipped).
    """
    logger = logging.getLogger(__name__)
    try:
        # 1. Parse Request and Extract Parameters.
        body = await request.json()
        # logger.info(f"Request Body: {body}")
        
        ticker = request.path_params.get("ticker")
        concept_name = body.get("concept")
        concept_label = body.get("label", concept_name)
        unit = body.get("unit", "USD")
        try:
            horizon = int(body.get("h", 12))
        except ValueError:
            raise HTTPException(status_code=400, detail="Invalid forecast horizon value.")
        data = body.get("data")
        inferred_freq = body.get("inferred_freq")
        name = body.get("name")
        companyName = body.get("companyName")
        exchange = body.get("exchange")
        cik = body.get("cik")

        # Optional flag: forecast (default: True)
        do_forecast = body.get("forecast", True)
        if not isinstance(do_forecast, bool):
            do_forecast = str(do_forecast).lower() in ["true", "1", "yes"]
        
        logger.info(f"Extracted Parameters: Ticker={ticker}, Concept={concept_name}, Concept Label={concept_label}, Unit={unit}, Horizon={horizon}, Forecast={do_forecast}")
        if not ticker or not concept_name:
            return JSONResponse({"error": "Ticker and concept are required."}, status_code=400)
        
        # 2. Validate API Authentication Headers.
        timegen_endpoint = request.headers.get("X-Timegen-Endpoint")
        timegen_key = request.headers.get("Authorization")
        if not timegen_endpoint or not timegen_key:
            return JSONResponse({"error": "Missing required headers: X-Timegen-Endpoint or Authorization"}, status_code=400)
        
        # 3. Process the XBRL Data.
        original_df = pd.DataFrame(data)
        # Preserve the original fetched data.
        historical_df = original_df.copy()
        historical_df["Type"] = "Historical"
        
        # Clean and infer frequency (using your helper function).
        cleaned_df, inferred_freq = clean_and_infer_frequency(original_df, ticker, concept_name)
        logger.info(f"Inferred Frequency: {inferred_freq}")
        
        # 4. Forecast Data (if requested).
        if do_forecast:
            nixtla_client = NixtlaClient(api_key=timegen_key, base_url=timegen_endpoint)
            if inferred_freq.lower().startswith("q"):
                horizon = 4
            elif inferred_freq.lower().startswith("a"):
                horizon = 1
            logger.info(f"Using forecast horizon: {horizon}")
            forecast_df = nixtla_client.forecast(df=cleaned_df, h=horizon, time_col="enddate", target_col="value")
            
            forecast_df["Type"] = "Forecast"
            if "timestamp" in forecast_df.columns:
                forecast_df.rename(columns={"timestamp": "enddate"}, inplace=True)
            if "TimeGPT" in forecast_df.columns:
                forecast_df.rename(columns={"TimeGPT": "forecastedvalue"}, inplace=True)
            if "forecastedvalue" not in forecast_df.columns and "value" in forecast_df.columns:
                forecast_df.rename(columns={"value": "forecastedvalue"}, inplace=True)
            
            try:
                last_historical_value = cleaned_df["value"].iloc[-1]
            except Exception:
                last_historical_value = forecast_df["forecastedvalue"].iloc[0]
            forecast_df["change_percent"] = ((forecast_df["forecastedvalue"] - last_historical_value) /
                                             last_historical_value * 100).round(2)
            forecast_df["trend"] = forecast_df["change_percent"].apply(
                lambda cp: "upward" if cp > 0 else ("downward" if cp < 0 else "stable")
            )
            def confidence(row):
                cp = row["change_percent"]
                if abs(cp) > 20:
                    return "High Confidence"
                elif abs(cp) > 10:
                    return "Moderate Confidence"
                else:
                    return "Low Confidence"
            forecast_df["forecast_confidence"] = forecast_df.apply(confidence, axis=1)
            forecast_df["forecast_comment"] = forecast_df["trend"].map({
                "upward": "Bullish outlook",
                "downward": "Bearish outlook",
                "stable": "Stable outlook"
            })
        else:
            forecast_df = pd.DataFrame()  # Empty forecast DataFrame.
        
        # 5. Prepare DataFrames for Combined Output.
        orig_cols = ["enddate", "value", "Type", "AccessionNumber", "FiscalYear",
                     "FiscalPeriod", "FormType", "FilingDate", "Reporting Period", "Concept"]
        fcst_cols = ["forecastedvalue", "change_percent", "trend", "forecast_confidence", "forecast_comment"]
        all_cols = orig_cols + fcst_cols
        
        for col in fcst_cols:
            if col not in historical_df.columns:
                historical_df[col] = None
        for col in orig_cols:
            if col not in forecast_df.columns:
                forecast_df[col] = None
            forecast_df["Concept"] = concept_name
        
        historical_df = historical_df.reindex(columns=all_cols)
        forecast_df = forecast_df.reindex(columns=all_cols)
        if forecast_df.empty:
            combined_df = historical_df.copy()
        else:
            combined_df = pd.concat([historical_df, forecast_df], ignore_index=True, sort=False)
        
        # Replace NaNs and sanitize data.
        historical_df = historical_df.replace({np.nan: None})
        forecast_df = forecast_df.replace({np.nan: None})
        combined_df = combined_df.replace({np.nan: None})
        historical_df = sanitize_df(historical_df)
        forecast_df = sanitize_df(forecast_df)
        combined_df = sanitize_df(combined_df)
        historical_df = convert_dates(historical_df)
        forecast_df = convert_dates(forecast_df)
        combined_df = convert_dates(combined_df)
        
        original_data_json = historical_df.to_dict(orient="records")
        forecast_data_json = forecast_df.to_dict(orient="records") if do_forecast and not forecast_df.empty else []
        combined_data_json = combined_df.to_dict(orient="records")
        original_data_json = [replace_nan_in_dict(rec) for rec in original_data_json]
        forecast_data_json = [replace_nan_in_dict(rec) for rec in forecast_data_json]
        combined_data_json = [replace_nan_in_dict(rec) for rec in combined_data_json]
        
        combined_data_csv = combined_df.to_csv(index=False)
        
        # 6. Create Plotly Figures.
        # --- Figure 1: Combined Plot (Historical and, if available, Forecast) ---
        var_hist_plot = historical_df[["enddate", "value"]].rename(columns={"value": "orig_value"})
        var_hist_plot["enddate"] = pd.to_datetime(var_hist_plot["enddate"], format="%Y-%m-%d", errors="coerce")
        var_hist_plot["value_millions"] = var_hist_plot["orig_value"] / 1e6

        fig_combined = go.Figure()
        fig_combined.add_trace(go.Scatter(
            x=var_hist_plot["enddate"],
            y=var_hist_plot["value_millions"],
            mode="lines+markers",
            name="Historical",
            line=dict(color="royalblue", width=2),
            marker=dict(size=6),
            hovertemplate=f"<b>Date</b>: %{{x|%Y-%m-%d}}<br><b>{concept_label}</b>: $%{{y:.2f}}M<extra></extra>"
        ))
        
        if do_forecast and not forecast_df.empty:
            var_fcst_plot = forecast_df[["enddate", "forecastedvalue"]].rename(columns={"forecastedvalue": "fcst_value"})
            var_fcst_plot["enddate"] = pd.to_datetime(var_fcst_plot["enddate"], format="%Y-%m-%d", errors="coerce")
            var_fcst_plot["value_millions"] = var_fcst_plot["fcst_value"] / 1e6
            fig_combined.add_trace(go.Scatter(
                x=var_fcst_plot["enddate"],
                y=var_fcst_plot["value_millions"],
                mode="lines+markers",
                name="Forecast",
                line=dict(color="firebrick", width=2, dash="dash"),
                marker=dict(size=6),
                hovertemplate=f"<b>Date</b>: %{{x|%Y-%m-%d}}<br><b>{concept_label}</b>: $%{{y:.2f}}M<extra></extra>"
            ))
        
        # Set x-axis boundaries.
        if do_forecast and not forecast_df.empty:
            x_min_plot = min(var_hist_plot["enddate"].min(), var_fcst_plot["enddate"].min())
            x_max_plot = max(var_hist_plot["enddate"].max(), var_fcst_plot["enddate"].max())
        else:
            x_min_plot = var_hist_plot["enddate"].min()
            x_max_plot = var_hist_plot["enddate"].max()
        bands = pd.date_range(start=x_min_plot, end=x_max_plot, freq="Q")
        toggle = True
        prev_date = x_min_plot
        for band_end in bands:
            if toggle:
                fig_combined.add_vrect(
                    x0=prev_date,
                    x1=band_end,
                    fillcolor="LightGray",
                    opacity=0.3,
                    line_width=0
                )
            toggle = not toggle
            prev_date = band_end
        if toggle and prev_date < x_max_plot:
            fig_combined.add_vrect(
                x0=prev_date,
                x1=x_max_plot,
                fillcolor="LightGray",
                opacity=0.3,
                line_width=0
            )
        if do_forecast and not forecast_df.empty:
            for idx, row in var_fcst_plot.iterrows():
                y_val = row["value_millions"]
                if pd.notnull(y_val):
                    fig_combined.add_hline(
                        y=y_val,
                        line_dash="dot",
                        line_color="gray",
                        opacity=0.5,
                        annotation_text=f"${y_val:.2f}M",
                        annotation_position="right",
                        annotation_yshift=10
                    )
        combined_title = (
            f"{ticker}: {concept_label} Historical"
            + (" and Forecast Data" if do_forecast and not forecast_df.empty else " Data")
        )
        combined_title = split_title(combined_title, threshold=50)
        fig_combined.update_layout(
            title={
                "text": combined_title,
                "y": 0.95,
                "x": 0.5,
                "xanchor": "center",
                "yanchor": "top"
            },
            xaxis_title="Date",
            yaxis=dict(
                title="Value (Million USD)",
                range=[
                    min(var_hist_plot["value_millions"].min(), 
                        var_fcst_plot["value_millions"].min() if do_forecast and not forecast_df.empty else var_hist_plot["value_millions"].min()) * 0.9,
                    max(var_hist_plot["value_millions"].max(), 
                        var_fcst_plot["value_millions"].max() if do_forecast and not forecast_df.empty else var_hist_plot["value_millions"].max()) * 1.1
                ],
                showgrid=True,
                gridcolor="LightGray",
                gridwidth=1,
                linecolor="black",
                ticks="outside"
            ),
            template="plotly_white",
            plot_bgcolor="white",
            legend=dict(
                orientation="h",
                yanchor="top",
                y=-0.15,
                xanchor="center",
                x=0.5
            ),
            margin=dict(t=80, b=150)
        )
        # Add a copyright annotation.
        fig_combined.add_annotation(
            xref="paper", yref="paper",
            x=0.99,
            y=-0.05,
            text="© 2025, AITrailblazer, powered by Filing Scout AI",
            showarrow=False,
            font=dict(size=10, color="gray"),
            xanchor="right",
            yanchor="top"
        )
        if do_forecast and not forecast_df.empty:
            forecast_start_date = var_fcst_plot["enddate"].min()
            forecast_start_value = var_fcst_plot.loc[var_fcst_plot["enddate"] == forecast_start_date, "value_millions"].values[0]
            fig_combined.add_annotation(
                x=forecast_start_date,
                y=forecast_start_value,
                text="Forecast Begins",
                showarrow=True,
                arrowhead=2,
                ax=0,
                ay=-30
            )
        
        # ----- NEW: Add Company Information Block to Combined Figure -----
        company_info = (
            f"<b>{ticker}</b><br>"
            f"<b>{companyName}</b><br>"
            f"Exchange: {exchange}<br>"
            f"CIK: {cik}"
        )
        fig_combined.add_annotation(
            xref="paper",
            yref="paper",
             x=0.0,        # Exactly at the left edge (or use 0.01 if you prefer a small margin)
            y=1.0,        # Exactly at the top edge (or use 0.99 for a slight offset)
            xanchor="left",
            yanchor="top",
            text=company_info,
            showarrow=False,
            align="left",
            font=dict(size=12, color="black"),
            bordercolor="black",
            borderwidth=1,
            borderpad=4,
            bgcolor="rgba(255, 255, 255, 0.8)"  # semi-transparent background
        )
        # -------------------------------------------------

        # Optional: Write the combined figure HTML to disk.
        #html_combined = fig_combined.to_html(full_html=True)
        #with open("html_combined.html", "w", encoding="utf-8") as f:
        #    f.write(html_combined)

        partial_html_combined = fig_combined.to_html(full_html=False)
        
        # --- Figure 2: Forecast-Only Figure (if forecast performed) ---
        if do_forecast and not forecast_df.empty:
            fcst_min = var_fcst_plot["value_millions"].min()
            fcst_max = var_fcst_plot["value_millions"].max()
            fcst_range_val = fcst_max - fcst_min
            narrow_threshold = 0.1  # million USD
            expansion_factor = 50 if (fcst_range_val < narrow_threshold) else 1
            var_fcst_plot["fcst_value_trans"] = var_fcst_plot["value_millions"] * expansion_factor

            fig_forecast = go.Figure()
            fig_forecast.add_trace(go.Scatter(
                x=var_fcst_plot["enddate"],
                y=var_fcst_plot["fcst_value_trans"],
                mode="lines+markers",
                name="Forecast",
                line=dict(color="firebrick", width=2, dash="dash"),
                marker=dict(size=6),
                customdata=var_fcst_plot["value_millions"],
                hovertemplate=f"<b>Date</b>: %{{x|%Y-%m-%d}}<br><b>{concept_label}</b>: $%{{customdata:.2f}}M<extra></extra>"
            ))
            x_min_fore = var_fcst_plot["enddate"].min()
            x_max_fore = var_fcst_plot["enddate"].max()
            bands_fore = pd.date_range(start=x_min_fore, end=x_max_fore, freq="Q")
            toggle = True
            prev_date = x_min_fore
            for band_end in bands_fore:
                if toggle:
                    fig_forecast.add_vrect(
                        x0=prev_date,
                        x1=band_end,
                        fillcolor="LightGray",
                        opacity=0.3,
                        line_width=0
                    )
                toggle = not toggle
                prev_date = band_end
            if toggle and prev_date < x_max_fore:
                fig_forecast.add_vrect(
                    x0=prev_date,
                    x1=x_max_fore,
                    fillcolor="LightGray",
                    opacity=0.3,
                    line_width=0
                )
            for idx, row in var_fcst_plot.iterrows():
                y_val_trans = row["fcst_value_trans"]
                if pd.notnull(y_val_trans):
                    fig_forecast.add_hline(
                        y=y_val_trans,
                        line_dash="dot",
                        line_color="gray",
                        opacity=0.5,
                        annotation_text=f"${row['value_millions']:.2f}M",
                        annotation_position="right",
                        annotation_yshift=10
                    )
            trans_min = var_fcst_plot["value_millions"].min() * expansion_factor
            trans_max = var_fcst_plot["value_millions"].max() * expansion_factor
            forecast_y_range = [trans_min * 0.95, trans_max * 1.05]

            fig_forecast.update_layout(
                title={
                    "text": (
                        f"{ticker}: {concept_label} Forecast (Expanded Scale)<br>"
                        f"(Forecast values multiplied by {expansion_factor}x)<br>"
                    ),
                    "y": 0.95,
                    "x": 0.5,
                    "xanchor": "center",
                    "yanchor": "top"
                },
                xaxis_title="Date",
                yaxis=dict(
                    title="Forecast Value (Expanded, Million USD)",
                    range=forecast_y_range,
                    showgrid=True,
                    gridcolor="LightGray",
                    gridwidth=1,
                    linecolor="black",
                    ticks="outside"
                ),
                template="plotly_white",
                plot_bgcolor="white",
                legend=dict(
                    orientation="h",
                    yanchor="top",
                    y=-0.15,
                    xanchor="center",
                    x=0.5
                ),
                margin=dict(t=80, b=150)
            )
            fig_forecast.add_annotation(
                xref="paper", yref="paper",
                x=0.99,
                y=-0.05,
                text="© 2025, AITrailblazer, powered by Filing Scout AI",
                showarrow=False,
                font=dict(size=10, color="gray"),
                xanchor="right",
                yanchor="top"
            )
            forecast_start_date = var_fcst_plot["enddate"].min()
            forecast_start_value_trans = var_fcst_plot.loc[var_fcst_plot["enddate"] == forecast_start_date, "fcst_value_trans"].values[0]
            fig_forecast.add_annotation(
                x=forecast_start_date,
                y=forecast_start_value_trans,
                text="Forecast Begins",
                showarrow=True,
                arrowhead=2,
                ax=0,
                ay=-30
            )
            # ----- NEW: Add Company Information Block to Forecast Figure -----
            fig_forecast.add_annotation(
                xref="paper",
                yref="paper",
                x=0.0,
                y=1.0,
                xanchor="left",
                yanchor="top",
                text=company_info,
                showarrow=False,
                align="left",
                font=dict(size=12, color="black"),
                bordercolor="black",
                borderwidth=1,
                borderpad=4,
                bgcolor="rgba(255, 255, 255, 0.8)"
            )
            # -------------------------------------------------
            partial_html_forecast = fig_forecast.to_html(full_html=False)
        else:
            partial_html_forecast = ""  # No forecast plot if forecast is skipped.
        
        # 7. Export combined data as CSV.
        combined_data_csv = combined_df.to_csv(index=False)
        combined_data_json = json.dumps(combined_data_json, indent=4)
        # logger.info(f"combined_data_json: {combined_data_json}")

        # --- Return JSON with both plot HTML snippets and data outputs ---
        response_data = {
            "ticker": ticker,
            "concept": concept_name,
            "concept_label": concept_label,
            "unit": unit,
            "original_data_json": json.dumps(original_data_json, indent=4),
            "forecast_data_json": json.dumps(forecast_data_json, indent=4),
            "combined_data_json": combined_data_json,
            "combined_data_csv": combined_data_csv,
            "combined_plot_html": partial_html_combined,
            "forecast_plot_html": partial_html_forecast
        }
        
        return JSONResponse(response_data)
    
    except Exception as e:
        logger.error(f"Unexpected error during forecasting plot: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)
        
def map_timedelta_to_freq(delta):
    """
    Map a typical gap (delta) to a standard frequency string.
    This example uses some heuristics:
      - ~7 days    => Weekly ('W')
      - ~30 days   => Monthly ('M')
      - ~90 days   => Quarterly ('Q')
      - ~365 days  => Annual ('A')
    You can adjust these thresholds as needed.
    """
    days = delta / pd.Timedelta(days=1)
    if abs(days - 7) < 1:
        return "W"
    elif 27 <= days <= 32:
        return "M"
    elif abs(days - 91) < 3 or abs(days - 92) < 3 or abs(days - 91.25) < 3:
        return "Q"
    elif abs(days - 365) < 10:
        return "A"
    else:
        return None

def clean_and_infer_frequency(df, ticker, concept_name, min_valid_rows=5, tolerance=1.5):
    """
    Clean the XBRL dataframe and infer the time frequency from the largest contiguous
    block of timestamps with "regular" intervals.
    
    Parameters:
      df : DataFrame
         Must contain at least "enddate" and "value" columns.
      ticker : str
         Ticker symbol (used for adding a unique_id column).
      concept_name : str
         For logging purposes.
      min_valid_rows : int
         Minimum number of rows required for a block to be considered.
      tolerance : float
         Multiplier applied to the median gap. Any gap larger than tolerance*median_gap
         is treated as a break in regularity.
    
    Returns:
      (df_clean, inferred_freq) where df_clean is the cleaned DataFrame (with columns:
      unique_id, enddate, and value) and inferred_freq is the inferred frequency string.
    """
    # Convert enddate to datetime, drop missing or duplicate dates, and sort.
    df["enddate"] = pd.to_datetime(df["enddate"], errors="coerce")
    df = df.dropna(subset=["enddate"]).sort_values("enddate").drop_duplicates(subset=["enddate"])
    if df.empty or len(df) < min_valid_rows:
        logger.error(f"Not enough valid dates for {ticker} - {concept_name}")
        return None, None
    df = df.copy()  # avoid modifying original dataframe
    df.set_index("enddate", inplace=True)
    
    # Compute gaps between consecutive timestamps.
    diffs = df.index.to_series().diff().dropna()
    if diffs.empty:
        logger.error(f"Insufficient differences in dates for {ticker} - {concept_name}")
        return None, None

    median_gap = diffs.median()
    gap_threshold = tolerance * median_gap
    logger.info(f"Median gap: {median_gap}, using gap threshold of {gap_threshold}")

    # Identify contiguous blocks where each consecutive gap is less than the threshold.
    timestamps = df.index
    blocks = []
    start_idx = 0
    for i in range(1, len(timestamps)):
        gap = timestamps[i] - timestamps[i - 1]
        # If the gap is too large, consider that a break between blocks.
        if gap > gap_threshold:
            block = timestamps[start_idx:i]
            if len(block) >= min_valid_rows:
                blocks.append(block)
            start_idx = i  # start a new block
    # Add the final block if it meets the criteria.
    final_block = timestamps[start_idx:]
    if len(final_block) >= min_valid_rows:
        blocks.append(final_block)

    if not blocks:
        logger.error(f"No contiguous block of sufficient length found for {ticker} - {concept_name}")
        return None, None

    # Choose the longest block.
    best_block = max(blocks, key=len)
    logger.info(f"Selected block from {best_block[0]} to {best_block[-1]} with {len(best_block)} rows.")

    # Use the block for inference.
    df_block = df.loc[best_block[0]: best_block[-1]]
    
    # Try built-in frequency inference.
    inferred_freq = pd.infer_freq(df_block.index)
    if inferred_freq is None:
        # Fallback: use the mode of the differences.
        block_diffs = df_block.index.to_series().diff().dropna()
        if not block_diffs.empty:
            mode_diff = block_diffs.mode()[0]
            mapped = map_timedelta_to_freq(mode_diff)
            if mapped:
                inferred_freq = mapped
                logger.info(f"Manual mapping: {mode_diff} mapped to frequency: {mapped}")
            else:
                # As a fallback, use the offset string from the mode difference.
                inferred_freq = str(pd.tseries.frequencies.to_offset(mode_diff))
                logger.info(f"Manual inference yielded offset: {inferred_freq}")
        else:
            logger.warning(f"Block differences empty for {ticker} - {concept_name}, defaulting to 'Q'")
            inferred_freq = "Q"
    else:
        logger.info(f"pd.infer_freq yielded: {inferred_freq}")

    # Log final inferred frequency.
    logger.info(f"clean_and_infer_frequency: Final inferred frequency for {ticker} - {concept_name}: {inferred_freq}")

    # Format dataframe for downstream processing.
    df_block = df_block.reset_index()
    df_block["unique_id"] = ticker
    df_clean = df_block[["unique_id", "enddate", "value"]]
    return df_clean, inferred_freq
    
async def anomalies_xbrl_data_plot(request: Request):
    """
    Endpoint to fetch XBRL data, detect anomalies using Nixtla, and return the results as JSON containing 
    both anomaly data and a Base64-encoded PNG plot.
    """
    ticker = request.path_params.get("ticker")
    concept_param = request.query_params.get("concept", "AssetsCurrent|Assets")
    unit = request.query_params.get("unit", "USD")

    try:
        # Validate ticker
        if not ticker:
            return JSONResponse({"error": "Ticker is required in the path parameters."}, status_code=400)

        # Split concept into name and label
        logger.info(f"Received concept_param: {concept_param}")
        concept_parts = concept_param.split("|")
        concept_name = concept_parts[0]
        concept_label = concept_parts[1].strip() if len(concept_parts) > 1 else concept_name

        logger.info(
            f"Starting anomaly detection for ticker: {ticker}, concept: {concept_name}, label: {concept_label}, unit: {unit}"
        )

        # Extract headers
        timegen_endpoint = request.headers.get("X-Timegen-Endpoint")
        timegen_key = request.headers.get("Authorization")

        if not timegen_endpoint or not timegen_key:
            return JSONResponse({"error": "Missing required headers: X-Timegen-Endpoint or Authorization."}, status_code=400)

        logger.info(f"Using TimegenEndpoint: {timegen_endpoint}")

        # Initialize Nixtla client
        nixtla_client = NixtlaClient(api_key=timegen_key, base_url=timegen_endpoint)

        # Fetch and process XBRL data
        xbrl_df = await fetch_xbrl_csv(ticker, concept_name, unit)
        if xbrl_df.empty:
            return JSONResponse({"error": f"No XBRL data found for ticker: {ticker}, concept: {concept_name}."}, status_code=404)

        # Prepare the dataframe for anomaly detection
        xbrl_df.rename(columns={"enddate": "timestamp", "value": "value"}, inplace=True)
        xbrl_df = xbrl_df.sort_values(by="timestamp").drop_duplicates(subset=["timestamp"])
        xbrl_df = xbrl_df[["timestamp", "value"]]

        # Infer frequency
        xbrl_df.set_index("timestamp", inplace=True)
        inferred_freq = pd.infer_freq(xbrl_df.index)

        if not inferred_freq or len(xbrl_df) < 48:
            logger.warning(f"Insufficient data or unable to infer frequency for ticker: {ticker}, concept: {concept_name}.")
            return JSONResponse({
                "ticker": ticker,
                "concept": concept_name,
                "unit": unit,
                "anomalies": [],
                "plot": None,
                "warning": "Insufficient data or unable to infer frequency."
            })

        logger.info(f"Inferred frequency: {inferred_freq}")

        # Reset index for compatibility
        xbrl_df.reset_index(inplace=True)

        # Detect anomalies using Nixtla
        anomalies_df = nixtla_client.detect_anomalies(
            df=xbrl_df, time_col="enddate", target_col="value", freq=inferred_freq
        )
        anomalies_json = anomalies_df.to_dict(orient="records") if not anomalies_df.empty else []

        logger.info(f"Anomalies detected: {len(anomalies_json)} records")

        # Generate the plot
        plot_fig = nixtla_client.plot(xbrl_df, anomalies_df, time_col="enddate", target_col="value")

        # Save the plot to a PNG in memory
        png_bytes = io.BytesIO()
        plot_fig.savefig(png_bytes, format="png", bbox_inches="tight")
        png_bytes.seek(0)

        # Encode PNG to Base64
        base64_png = base64.b64encode(png_bytes.getvalue()).decode("utf-8")

        # Build the JSON response
        response_data = {
            "ticker": ticker,
            "concept": concept_name,
            "unit": unit,
            "anomalies": anomalies_json,
            "plot": base64_png,
        }

        return JSONResponse(response_data)

    except Exception as e:
        logger.error(f"Unexpected error: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)
        
async def fetch_xbrl_csv(ticker, concept="AssetsCurrent", unit="USD"):
    """
    Function to fetch and process XBRL data as a Pandas DataFrame.
    """
    try:
        # logger.info(f"fetch_xbrl_csv Fetching XBRL data for ticker: {ticker}, concept: {concept}, unit: {unit}")

        # Fetch the CIK for the given ticker
        cik = company_info.get_cik_by_ticker(ticker)
        if not cik:
            logger.error(f"No CIK found for ticker: {ticker}")
            raise ValueError(f"CIK not found for ticker: {ticker}")

        # Fetch and process XBRL data
        xbrl_data = sec_filings.get_xbrl_data(cik)
        if not xbrl_data or "facts" not in xbrl_data:
            logger.error(f"No XBRL data found for CIK: {cik}")
            raise ValueError(f"No XBRL data found for ticker: {ticker}")

        xbrl_df = sec_filings.process_xbrl_data(xbrl_data, concept=concept, unit=unit)
        #  logger.info(f"XBRL data for ticker {ticker}:\n{xbrl_df.head()}")

        # Add the ticker as a column
        # xbrl_df["unique_id"] = ticker
        
        # Add the concept as a column
        xbrl_df["concept"] = concept

        # if 'start' in xbrl_df.columns and not xbrl_df['start'].isnull().all():
        #     xbrl_df['start'] = pd.to_datetime(xbrl_df['start'], errors='coerce')
        # xbrl_df['end'] = pd.to_datetime(xbrl_df['end']) 

        #          "end","val","accn","fy","fp","form","filed","frame"
        #  "start","end","val","accn","fy","fp","form","filed","frame"
        # Rename columns for better readability
        column_mapping = {
            "frame": "ReportingPeriod",
            "end": "enddate",
            "val": "value",
            "unit": "Unit",
            "concept": "Concept",
            "start": "StartDate",
            "accn": "AccessionNumber",
            "fy": "FiscalYear",
            "fp": "FiscalPeriod",
            "form": "FormType",
            "filed": "FilingDate"
        }
        xbrl_df.rename(columns=column_mapping, inplace=True)

        # Filter valid frames
        xbrl_df = xbrl_df[xbrl_df["Reporting Period"].notna()]
        if xbrl_df.empty:
            logger.error(f"XBRL data is empty for ticker: {ticker}, concept: {concept}")
            raise ValueError(f"No XBRL data available for ticker: {ticker}")

        # Log filtered data for debugging
        # logger.info(f"Processed XBRL data for ticker {ticker}:\n{xbrl_df.head()}")

        return xbrl_df

    except ValueError as ve:
        logger.error(f"Value error for ticker {ticker}: {ve}")
        raise ve
    except Exception as e:
        logger.error(f"fetch_xbrl_csv Unexpected error while fetching XBRL data for ticker {ticker}: {e}")
        raise e

async def fetch_xbrl_concept_json(ticker, concept="AssetsCurrent", unit="USD"):
    """
    Function to fetch XBRL data, log column names, and return data as JSON.
    Prints execution time on the terminal but does not include it in the response.
    """
    start_time = time.perf_counter()  # Start high-precision timer

    try:
        concept_parts = concept.split("|")
        concept_name = concept_parts[0]
        logger.info(f"fetch_xbrl_concept_json Received request: ticker={ticker}, concept={concept_name}, unit={unit}")

        # Fetch the CIK for the given ticker
        cik = company_info.get_cik_by_ticker(ticker)
        if not cik:
            logger.error(f"No CIK found for ticker: {ticker}")
            raise ValueError(f"CIK not found for ticker: {ticker}")

        # Fetch and process XBRL data
        xbrl_data = sec_filings.get_xbrl_data(cik)
        if not xbrl_data or "facts" not in xbrl_data:
            logger.error(f"No XBRL data found for CIK: {cik}")
            raise ValueError(f"No XBRL data found for ticker: {ticker}")

        # Process XBRL data into a DataFrame
        processing_start = time.perf_counter()  # Start timer for data processing
        xbrl_df = sec_filings.process_xbrl_data(xbrl_data, concept=concept_name, unit=unit)
        processing_duration_ms = (time.perf_counter() - processing_start) * 1000  # Time in ms

        # Add the concept as a column
        xbrl_df["concept"] = concept_name

        # Convert DataFrame to JSON
        xbrl_json = xbrl_df.to_dict(orient="records")

        # Measure total execution time
        total_duration_ms = (time.perf_counter() - start_time) * 1000  # Time in ms

        logger.info(f"Fetching fetch_xbrl_concept_json for ticker {ticker} took {total_duration_ms:.3f} ms")
        
        # **Print execution time on the terminal only**
        logger.info(f"[Terminal] Execution time for {ticker}: Total={total_duration_ms:.3f}ms, Processing={processing_duration_ms:.3f}ms")
        # Return JSON response **without execution time**
        return {
            "columns": list(xbrl_df.columns),
            "data": xbrl_json
        }

    except ValueError as ve:
        duration_ms = (time.perf_counter() - start_time) * 1000
        logger.error(f"Value error for ticker {ticker} (Took {duration_ms:.3f}ms): {ve}")
        print(f"[Terminal] Error: {ve} (Execution time: {duration_ms:.3f}ms)")
        raise ve

    except Exception as e:
        duration_ms = (time.perf_counter() - start_time) * 1000
        logger.error(f"Unexpected error for ticker {ticker} (Took {duration_ms:.3f}ms): {e}")
        print(f"[Terminal] Unexpected error: {e} (Execution time: {duration_ms:.3f}ms)")
        raise e
            
import json
from starlette.responses import JSONResponse, HTMLResponse
import plotly.express as px
import logging

logger = logging.getLogger(__name__)
    
    # Custom pretty-print JSON response
class PrettyJSONResponse(JSONResponse):
    def render(self, content) -> bytes:
        return json.dumps(content, indent=4).encode("utf-8")



class XBRLDataItem(BaseModel):
    Reporting_Period: Optional[str] = Field(None, alias="Reporting Period")
    End_Date: str = Field(..., alias="End Date")
    Value: float = Field(..., alias="Value")
    Unit: str = Field(..., alias="Unit")
    Concept: str = Field(..., alias="Concept")
    Start_Date: str = Field(..., alias="Start Date")
    Accession_Number: str = Field(..., alias="Accession Number")
    Fiscal_Year: int = Field(..., alias="Fiscal Year")
    Fiscal_Period: str = Field(..., alias="Fiscal Period")
    Form_Type: str = Field(..., alias="Form Type")
    Filing_Date: str = Field(..., alias="Filing Date")

    class Config:
        # For Pydantic V1 use:
        # allow_population_by_field_name = True
        # For Pydantic V2, uncomment the following line:
        populate_by_name = True

class XBRLData(BaseModel):
    available_units: List[str]
    data: List[XBRLDataItem]
    inferred_freq: str
    label: str
    name: str

    class Config:
        # For Pydantic V1 use:
        # allow_population_by_field_name = True
        # For Pydantic V2, uncomment the following line:
        populate_by_name = True

    
# Pydantic model for the overall payload
class XBRLData(BaseModel):
    available_units: List[str]
    data: List[XBRLDataItem]
    inferred_freq: str
    label: str
    name: str

    class Config:
        allow_population_by_field_name = True


    """
 the following concepts from your list are among the most significant:

Assets: Represents the total resources owned by the company.
AssetsCurrent: Denotes assets expected to be converted to cash or used within a year.
Liabilities: Indicates the company's total obligations.
LiabilitiesCurrent: Shows obligations the company needs to settle within a year.
StockholdersEquity: Reflects the residual interest in the company's assets after deducting liabilities.
Revenues: Measures the total income generated from the sale of goods or services.
CostOfGoodsAndServicesSold: Represents the direct costs attributable to the production of goods sold by the company.
GrossProfit: Calculated as Revenues minus CostOfGoodsAndServicesSold, indicating the efficiency of production and pricing.
OperatingIncomeLoss: Shows the profit or loss from primary business operations, excluding non-operational income and expenses.
NetIncomeLoss: Represents the company's total earnings or losses, providing insight into overall profitability.
EarningsPerShareBasic: Indicates the portion of a company's profit allocated to each outstanding share of common stock.
CashAndCashEquivalentsAtCarryingValue: Shows the company's liquidity position, essential for meeting short-term obligations.
LongTermDebt: Represents the total amount of debt due beyond one year, impacting the company's long-term financial stability.
ResearchAndDevelopmentExpense: Reflects the company's investment in innovation and future growth.
SellingGeneralAndAdministrativeExpense: Encompasses the overhead costs related to selling products and managing the business.
"""
async def fetch_xbrl_concept_json(request):
    """
    Endpoint to fetch XBRL data for a specific concept and return the data as JSON.
    Logs column names for inspection and includes them in the response.
    """
    ticker = request.path_params["ticker"]
    concept_param = request.query_params.get("concept", "AssetsCurrent")
    unit = request.query_params.get("unit", "USD")

    try:
        concept_parts = concept_param.split("|")
        concept_name = concept_parts[0]
        # concept_label = concept_parts[1] if len(concept_parts) > 1 else concept_name
        # logger.info(f"fetch_xbrl_concept_json Fetching XBRL data for ticker: {ticker}, concept: {concept_name}, unit: {unit}")

        # Fetch the CIK for the given ticker
        cik = company_info.get_cik_by_ticker(ticker)
        if not cik:
            logger.error(f"No CIK found for ticker: {ticker}")
            raise ValueError(f"CIK not found for ticker: {ticker}")

        # Fetch and process XBRL data
        xbrl_data = sec_filings.get_xbrl_data(cik)
        if not xbrl_data or "facts" not in xbrl_data:
            logger.error(f"No XBRL data found for CIK: {cik}")
            raise ValueError(f"No XBRL data found for ticker: {ticker}")

        # Process XBRL data into a DataFrame
        xbrl_df = sec_filings.process_xbrl_data(xbrl_data, concept=concept_name, unit=unit)
        #  logger.info(f"fetch_xbrl_concept_json XBRL data for ticker {ticker} fetched successfully.")

        # Log column names for debugging purposes
        column_names = list(xbrl_df.columns)
        #  logger.info(f"fetch_xbrl_concept_json Column names in XBRL data for ticker {ticker}: {column_names}")

        # Add the concept as a column for additional context
        xbrl_df["concept"] = concept_name

        # Convert the DataFrame to JSON format
        xbrl_json = xbrl_df.to_dict(orient="records")

        # Return a JSON response with column metadata and data
        response_data = {
            "columns": column_names,
            "data": xbrl_json
        }
        return JSONResponse(response_data)

    except ValueError as ve:
        logger.error(f"Value error for ticker {ticker}: {ve}")
        return JSONResponse({"error": str(ve)}, status_code=400)

    except Exception as e:
        logger.error(f"fetch_xbrl_concept_json Unexpected error while fetching XBRL data for ticker {ticker}: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)

async def list_xbrl_concepts_as_json1(request: Request):
    """
    Endpoint to list all available XBRL concepts and their fields for a given company ticker in JSON format.
    """
    ticker = request.path_params["ticker"]

    try:
        # Validate ticker format
        if not re.match(r"^[A-Za-z0-9]+$", ticker):
            raise ValueError("Invalid ticker format. Only alphanumeric characters are allowed.")

        # Get CIK for the ticker
        cik = company_info.get_cik_by_ticker(ticker)
        if not cik:
            raise ValueError(f"CIK not found for ticker: {ticker}")

        # Fetch XBRL data
        xbrl_data = sec_filings.get_xbrl_data(cik)

        # Extract available concepts with their details
        concepts = xbrl_data.get("facts", {}).get("us-gaap", {})
        if not concepts:
            raise ValueError(f"No XBRL concepts found for ticker: {ticker}")

        # Build a list of concept details
        concept_details = []
        for concept_name, details in concepts.items():
            concept_details.append({
                "name": concept_name,
                "label": details.get("label", ""),
                # "type": details.get("type", ""),
                # "periodType": details.get("periodType", ""),
                # "balance": details.get("balance", ""),
                # "documentation": details.get("documentation", "")
            })

        return JSONResponse({"ticker": ticker, "concepts": concept_details})
    except ValueError as ve:
        logger.error(f"Value error for ticker {ticker}: {ve}")
        return JSONResponse({"error": str(ve)}, status_code=400)
    except Exception as e:
        logger.error(f"Unexpected error for ticker {ticker}: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)
    
async def list_xbrl_concepts_as_json(request: Request):
    """
    Endpoint to list all available XBRL concepts and their fields for a given company ticker in JSON format.
    Includes inferred frequency for each concept and streams the results concept by concept with execution time.
    Execution time is logged in the console.
    """
    ticker = request.path_params["ticker"]

    async def concept_stream():
        try:
            overall_start = time.perf_counter()  # High-precision timer
            logger.info(f"Starting processing for ticker: {ticker}")

            # Validate ticker format
            if not re.match(r"^[A-Za-z0-9]+$", ticker):
                logger.error(f"Invalid ticker format: {ticker}")
                yield json.dumps({"error": "Invalid ticker format. Only alphanumeric characters are allowed."}) + "\n"
                return

            # Get CIK for the ticker
            cik = company_info.get_cik_by_ticker(ticker)
            if not cik:
                logger.error(f"CIK not found for ticker: {ticker}")
                yield json.dumps({"error": f"CIK not found for ticker: {ticker}"}) + "\n"
                return

            # Fetch XBRL data
            xbrl_data = sec_filings.get_xbrl_data(cik)

            # Extract available concepts
            concepts = xbrl_data.get("facts", {}).get("us-gaap", {})
            if not concepts:
                logger.error(f"No XBRL concepts found for ticker: {ticker}")
                yield json.dumps({"error": f"No XBRL concepts found for ticker: {ticker}"}) + "\n"
                return

            logger.info(f"Found {len(concepts)} concepts for ticker: {ticker}")

            # Loop through each concept, sorting and limiting to top 50
            for concept_name, details in sorted(concepts.items())[:50]:
            # for concept_name, details in sorted(concepts.items()):
                concept_start_time = time.perf_counter()  # Start time for each concept

                try:
                    # Skip deprecated concepts
                    label = details.get("label", "")
                    if "Deprecated" in label:
                        continue

                    # Get available units for the concept
                    available_units = list(details.get("units", {}).keys())

                    # Ensure USD is available
                    if "USD" not in available_units:
                        logger.warning(f"Skipping {concept_name}: USD unit not available")
                        continue  # Skip if no USD data

                    # Fetch and process XBRL data for the concept
                    xbrl_df = await fetch_xbrl_csv(ticker, concept_name, "USD")

                    # Ensure timestamps are sorted and infer frequency
                    xbrl_df = xbrl_df.sort_values(by="enddate").drop_duplicates(subset=["enddate"])
                    xbrl_df.set_index("enddate", inplace=True)

                    # Infer frequency
                    inferred_freq = pd.infer_freq(xbrl_df.index)

                    concept_duration_ms = (time.perf_counter() - concept_start_time) * 1000  # Execution time in ms

                    # Log execution time in console
                    # logger.info(
                    #     f"Processed {concept_name} (Label: {label}, Available Units: {available_units}) "
                    #     f"in {round(concept_duration_ms, 3)} ms"
                    # )

                    # Stream concept details
                    yield json.dumps({
                        "name": concept_name,
                        "label": label,
                        "available_units": available_units,
                        "inferred_freq": bool(inferred_freq)  # True if frequency inference succeeds
                    }) + "\n"

                except Exception as e:
                    concept_duration_ms = (time.perf_counter() - concept_start_time) * 1000

                    # Log error in console
                    logger.error(f"Error processing {concept_name}: {e} (Execution time: {round(concept_duration_ms, 3)} ms)")

                    # Stream error for the specific concept
                    yield json.dumps({
                        "name": concept_name,
                        "label": details.get("label", ""),
                        "available_units": available_units,
                        "inferred_freq": False,  # False in case of error
                        "error": str(e)
                    }) + "\n"

            overall_duration_ms = (time.perf_counter() - overall_start) * 1000
            logger.info(f"Completed processing for ticker {ticker} in {round(overall_duration_ms, 3)} ms")

            yield json.dumps({"message": "Processing complete"}) + "\n"

        except Exception as e:
            error_msg = f"An unexpected error occurred: {str(e)}"
            logger.error(error_msg)
            yield json.dumps({"error": error_msg}) + "\n"

    # Return streaming response
    return StreamingResponse(concept_stream(), media_type="application/json")

async def list_xbrl_concepts_as_csv(request: Request):
    """
    Endpoint to list all available XBRL concepts and their fields for a given company ticker as a CSV.
    """
    ticker = request.path_params["ticker"]

    try:
        # Validate ticker format
        if not re.match(r"^[A-Za-z0-9]+$", ticker):
            raise ValueError("Invalid ticker format. Only alphanumeric characters are allowed.")

        # Get CIK for the ticker
        cik = company_info.get_cik_by_ticker(ticker)
        if not cik:
            raise ValueError(f"CIK not found for ticker: {ticker}")

        # Fetch XBRL data
        xbrl_data = sec_filings.get_xbrl_data(cik)

        # Extract available concepts with all their details
        concepts = xbrl_data.get("facts", {}).get("us-gaap", {})
        if not concepts:
            raise ValueError(f"No XBRL concepts found for ticker: {ticker}")

        # Create a CSV buffer
        output = StringIO()
        writer = csv.writer(output)

        # Write the header row (adjust fields as needed)
        header = ["Concept Name", "Label", "Type", "Period Type", "Balance Type", "Documentation"]
        writer.writerow(header)

        # Write data rows for each concept
        for concept_name, concept_details in concepts.items():
            # Extract details (if available; fallback to empty string if not)
            label = concept_details.get("label", "")
            # concept_type = concept_details.get("type", "")
            # period_type = concept_details.get("periodType", "")
            # balance = concept_details.get("balance", "")
            # documentation = concept_details.get("documentation", "")

            writer.writerow([concept_name, label])

        # Reset buffer position
        output.seek(0)

        # Return CSV as a streaming response
        headers = {
            "Content-Disposition": f"attachment; filename={ticker}_xbrl_concepts_full.csv"
        }
        return StreamingResponse(output, media_type="text/csv", headers=headers)

    except ValueError as ve:
        logger.error(f"Value error for ticker {ticker}: {ve}")
        return JSONResponse({"error": str(ve)}, status_code=400)
    except Exception as e:
        logger.error(f"Unexpected error for ticker {ticker}: {e}")
        return JSONResponse({"error": f"An unexpected error occurred: {str(e)}"}, status_code=500)
    
async def download_html_content_route(request: Request):
    """
    Route to download HTML content for a given filing based on POST request data.
    """
    try:
        # Parse JSON body for required parameters
        body = await request.json()
        cik = body.get("cik")
        accession_number = body.get("accession_number").replace("-", "")
        primary_document = body.get("primary_document")

        # Validate required fields
        if not cik or not accession_number or not primary_document:
            return JSONResponse(
                {"error": "Missing required fields: cik, accession_number, primary_document"},
                status_code=400
            )

        # Construct the full URL using the primary document as is
        base_url = f"https://www.sec.gov/Archives/edgar/data/{cik}/{accession_number}/"
        full_url = f"{base_url}{primary_document}"

        # Log the constructed URL
        logger.info(f"Constructed URL: {full_url}")

        # Fetch the document from the constructed URL
        response = requests.get(full_url, headers={"User-Agent": "YourAppName (your_email@example.com)"})

        # Check if the request was successful
        if response.status_code != 200:
            return JSONResponse(
                {"error": f"Failed to retrieve document from {full_url}. Status code: {response.status_code}"},
                status_code=response.status_code
            )

        # Modify the HTML content to fix relative image links
        html_content = re.sub(
            r'(?<=<img src=")([^":]+)',  # Match the src attribute of <img> tags that do not contain a full URL
            lambda match: f"{base_url}{match.group(1)}",  # Replace with the full URL
            response.text
        )

        # Return the modified HTML content as a streaming response
        headers = {"Content-Disposition": f"attachment; filename={primary_document}"}
        return StreamingResponse(BytesIO(html_content.encode("utf-8")), media_type="text/html", headers=headers)

    except Exception as e:
        logger.error(f"Unexpected error: {e}")
        return JSONResponse(
            {"error": f"An unexpected error occurred: {str(e)}"},
            status_code=500
        )
                      
async def root(request):
    """Root endpoint that says Hello."""
    return PlainTextResponse("Hello")

# Define routes
routes = [
    Route("/", root),  # Root endpoint
    Route("/cik/{ticker}", get_cik),  # Fetch the CIK (Central Index Key) for a ticker
    Route("/name/{ticker}", get_name),  # Fetch the CIK (Central Index Key) for a ticker
    Route("/exchanges", get_all_exchanges, methods=["GET"]),  # Fetch all unique exchanges
    Route("/exchange/{ticker}", get_exchange),  # Fetch the CIK (Central Index Key) for a ticker
    Route("/tickersbyexchange/{exchange}", tickers_by_exchange, methods=["GET"]),  # Fetch tickers for a specific exchange
    Route("/filings/{ticker}", get_filings),  # Fetch all filings for a ticker
    Route("/forms/{ticker}", get_available_forms, methods=["GET"]),
    Route("/filing/html", download_latest_filing_html, methods=["POST"]),  # Change to POST request#    
    Route("/filing/pdf/{ticker}/{form_type}", download_latest_filing_pdf, methods=["GET"]),    
    Route("/xbrl/{ticker}/data", get_xbrl_data),  # Fetch processed XBRL data as JSON    
    Route("/xbrl/{ticker}/json", fetch_xbrl_concept_json),  # Generate and download XBRL data as JSON    
    Route("/xbrl/concepts/{ticker}/json", list_xbrl_concepts_as_json, methods=["GET"]),
    Route("/xbrl/concepts/{ticker}/csv", list_xbrl_concepts_as_csv, methods=["GET"]),
    Route("/xbrl/{ticker}/csv", fetch_xbrl_csv),  # Generate and download XBRL data as CSV    
    Route("/xbrl/{ticker}/forecast/csv", forecast_xbrl_data_as_csv),  # Fetch, forecast, and return forecasted data
    Route("/xbrl/{ticker}/forecast/plot", forecast_xbrl_data_plot, methods=["POST"]),  # Fetch, forecast, and return forecasted data as Plot
    Route("/xbrl/{ticker}/anomalies/plot", anomalies_xbrl_data_plot),  # Fetch, forecast, and return forecasted data as Plot
    Route("/html/download", download_html_content_route, methods=["POST"]),
]

# Create Starlette app
app = Starlette(debug=True, routes=routes)

if __name__ == "__main__":
    port = int(os.getenv("PORT", 8000))
    import uvicorn
    uvicorn.run("main:app", host="0.0.0.0", port=port, log_level="info")
