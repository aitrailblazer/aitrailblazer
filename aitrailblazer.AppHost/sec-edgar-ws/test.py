import urllib.request
import json
import os
import ssl

def allowSelfSignedHttps(allowed):
    # bypass the server certificate verification on client side
    if allowed and not os.environ.get('PYTHONHTTPSVERIFY', '') and getattr(ssl, '_create_unverified_context', None):
        ssl._create_default_https_context = ssl._create_unverified_context

allowSelfSignedHttps(True) # this line is needed if you use self-signed certificate in your scoring service.

# Request data goes here
# The example below assumes JSON formatting which may be updated
# depending on the format your endpoint expects.
# More information can be found here:
# https://docs.microsoft.com/azure/machine-learning/how-to-deploy-advanced-entry-script
data = {
  "freq": "D",
  "fh": 7,
  "y": {
    "2015-12-02": 8.71177264560569,
    "2015-12-03": 8.05610965954506,
    "2015-12-04": 8.08147504013705,
    "2015-12-05": 7.45876269238096,
    "2015-12-06": 8.01400499477946,
    "2015-12-07": 8.49678638163858,
    "2015-12-08": 7.98104975966596,
    "2015-12-09": 7.77779262633883,
    "2015-12-10": 8.2602342916073,
    "2015-12-11": 7.86633892304654,
    "2015-12-12": 7.31055015853442,
    "2015-12-13": 7.71824095195932,
    "2015-12-14": 8.31947369244219,
    "2015-12-15": 8.23668532271246,
    "2015-12-16": 7.80751004221619,
    "2015-12-17": 7.59186171488993,
    "2015-12-18": 7.52886925664225,
    "2015-12-19": 7.17165682276851,
    "2015-12-20": 7.89133075766189,
    "2015-12-21": 8.36007143564403,
    "2015-12-22": 8.11042723757502,
    "2015-12-23": 7.77527584648686,
    "2015-12-24": 7.34729970074316,
    "2015-12-25": 7.30182234213793,
    "2015-12-26": 7.12044437239249,
    "2015-12-27": 8.87877607170755,
    "2015-12-28": 9.25061821847475,
    "2015-12-29": 9.24792513230345,
    "2015-12-30": 8.39140318535794,
    "2015-12-31": 8.00469951054955,
    "2016-01-01": 7.58933582317062,
    "2016-01-02": 7.82524529143177,
    "2016-01-03": 8.24931374626064,
    "2016-01-04": 9.29514097366865,
    "2016-01-05": 8.56826646160024,
    "2016-01-06": 8.35255436947459,
    "2016-01-07": 8.29579811063615,
    "2016-01-08": 8.29029259122431,
    "2016-01-09": 7.78572089653462,
    "2016-01-10": 8.28172399041139,
    "2016-01-11": 8.4707303170059,
    "2016-01-12": 8.13505390861157,
    "2016-01-13": 8.06714903991011
  },
  "clean_ex_first": True,
  "finetune_steps": 0,
  "finetune_loss": "default"
}

body = str.encode(json.dumps(data))

url = 'https://TimeGEN-1-2.eastus.models.ai.azure.com/forecast'
# Replace this with the primary/secondary key, AMLToken, or Microsoft Entra ID token for the endpoint
api_key = 'T9eeweRb0sioyeAJYm6J68Ddp5EXG9Dq'
if not api_key:
    raise Exception("A key should be provided to invoke the endpoint")


headers = {'Content-Type':'application/json', 'Authorization':('Bearer '+ api_key)}

req = urllib.request.Request(url, body, headers)

try:
    response = urllib.request.urlopen(req)

    result = response.read()
    print(result)
except urllib.error.HTTPError as error:
    print("The request failed with status code: " + str(error.code))

    # Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
    print(error.info())
    print(error.read().decode("utf8", 'ignore'))