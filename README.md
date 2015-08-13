# Clink

Clink is a simple utility that monitors HTTP endpoints and reports if their 
up/down status changes. Multiple endpoints can be configured and can be 
checked at different intervals. If an endpoint's status changes then Clink can 
report it via email (or whatever other reporters you configure).

## Configuration

There are two things to configure: **Endpoints** and **Reporters**. They are 
configured in Endpoints.json and Reporters.json, respectively.

## Why the name *Clink*?

[Take a guess](http://www.thesaurus.com/browse/ping).

## Coming Soon

* Configure endpoints/reporters graphically.
* Plugin framework (write your own reporters).
* Save a history of all status checks.