# Redirecter

This repository contains an application for redirecting web-traffic. Based on an unique ID or an unique name the http api returns an _302 "Moved Temporarily"_ with the corresponding url to the website.

Use case: Create QR-Code or NFC Tags with an URL pointing to the redirecter api. Either the name or the id must be attached to this url. An request to the redirecter returns an _302 "Moved Temporarily"_ which instructs the browser to call the website in the body.

# Status of this reposity:

This application has an alpha status. **_Do not use it for production!_**

[![Test](https://github.com/thephonehouse/Redirecter/actions/workflows/test.yml/badge.svg)](https://github.com/thephonehouse/Redirecter/actions/workflows/test.yml) [![CodeQL](https://github.com/thephonehouse/Redirecter/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/thephonehouse/Redirecter/actions/workflows/github-code-scanning/codeql) [![Build & Deploy](https://github.com/thephonehouse/Redirecter/actions/workflows/docker-image.yml/badge.svg)](https://github.com/thephonehouse/Redirecter/actions/workflows/docker-image.yml)
