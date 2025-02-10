import pandas as pd
from io import StringIO
from nixtla import NixtlaClient

timegen_endpoint = "https://AITTimeGEN-1.eastus.models.ai.azure.com/"
timegen_key = "SNFqpAe05E19yV3ysBiaQUqhdN8m2Cs1"

# df = pd.read_csv('https://raw.githubusercontent.com/Nixtla/transfer-learning-time-series/main/datasets/air_passengers.csv')

# Define the data as a string
data_str = """\
timestamp     value
1961-01-01  436.843414
1961-02-01  419.351532
1961-03-01  458.943176
1961-04-01  477.876068
1961-05-01  505.656921"""

data_str = """\
unique_id   enddate      value
TSLA 2020-09-30  342000000
TSLA 2020-12-31  334000000
TSLA 2021-03-31  324000000
TSLA 2021-06-30  315000000
TSLA 2021-09-30  304000000
TSLA 2021-12-31  299000000
TSLA 2022-03-31  293000000
TSLA 2022-06-30  287000000
TSLA 2022-09-30  280000000
TSLA 2022-12-31  280000000
TSLA 2023-03-31  276000000
TSLA 2023-06-30  272000000
TSLA 2023-09-30  268000000
TSLA 2023-12-31  266000000
TSLA 2024-03-31  263000000
TSLA 2024-06-30  256000000
TSLA 2024-09-30  250000000"""

data_str = """\
unique_id   enddate      value
TSLA 2010-12-31    621935000
TSLA 2011-06-30    871174000
TSLA 2011-09-30    881941000
TSLA 2011-12-31    893336000
TSLA 2012-03-31    913040000
TSLA 2012-06-30    926981000
TSLA 2012-09-30    947693000
TSLA 2012-12-31   1190191000
TSLA 2013-03-31   1222825000
TSLA 2013-06-30   1714163000
TSLA 2013-09-30   1687397000
TSLA 2013-12-31   1806617000
TSLA 2014-03-31   2101352000
TSLA 2014-06-30   2203535000
TSLA 2014-09-30   2284010000
TSLA 2020-12-31  27260000000"""

# Convert the string data into a Pandas DataFrame
df = pd.read_csv(StringIO(data_str), delim_whitespace=True)

# Convert 'timestamp' column to datetime
df["enddate"] = pd.to_datetime(df["enddate"])

print(df.head())

nixtla_client = NixtlaClient(api_key=timegen_key, base_url=timegen_endpoint)

fcst = nixtla_client.forecast(
    df=df, 
    h=12, 
    time_col='enddate', 
    target_col='value')
# Print the full forecasted dataset
print("\nForecasted Data:\n", fcst.to_string())  # `.to_string()` prints the full DataFrame