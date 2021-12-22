# Example of KrakenD and JWT Validation

## Description

- `auth` and `resource` .net web api proxied through a KrakenD api gateway.
- you can directly run apis and krakend with ````docker-compose up```` command.(auth and resource images gonna build by default.)
- krakend directory contains configuration(for create token and validate it). for changing configuration you can browse `http://localhost:8787` and change them on krakend designer.
- create jwt token by using auth api with curl below.
    ````bash
    curl -X POST "http://localhost:8888/create_token"
    ````
- call resource api with jwt token with bearer header.
    ````bash
    curl -X GET "http://localhost:8888/weather_forecast" -H "Authorization: Bearer {token}"
    ````

For create RS256 key pair:
- for windows you can download OpenSSL from <a href="https://slproweb.com/products/Win32OpenSSL.html">this</a> link. for debians base distros you can install with ```` sudo apt install openssl ````
- after setup then search "Win64 OpenSSL Command Prompt" (for windows)
- when opens command prompt then you can use ````openssl genrsa -out rsa_4096_priv.pem 4096 ```` command for creating private key
- for creating public key you can use ```` openssl rsa -pubout -in rsa_4096_priv.pem -out rsa_4096_pub.pem ```` this command in same directory with private key
- after creating key pair then you can copy this keys to appsettings file without ````\n```` characters.

Note: login logic not added on token endpoint. purpose of this project just simple implementation of jwt validation on krakend.