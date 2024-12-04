# API Aggregation Project

## Overview

The API Aggregation Project is an ASP.NET Web API application that aggregates data from multiple external APIs into a single HTTP response. It demonstrates how to consume RESTful services and provides a structured way to retrieve and combine data from multiple sources (in this case, comments, posts and users).

## Features

- Aggregates data from different external APIs.
- Uses HttpClient to perform asynchronous HTTP requests.
- Implements Dependency Injection for better code maintainability.
- Includes unit tests for service methods using Moq.

## Technologies Used

- ASP.NET Core
- C#
- HttpClient
- Moq (for unit testing)
- Xunit (for unit testing)
- IConfiguration (for configuration management)

## External APIs

This project integrates with the following external APIs:

- [JSONPlaceholder - Comments API](https://jsonplaceholder.typicode.com/comments)
- [JSONPlaceholder - Posts API](https://jsonplaceholder.typicode.com/posts)
- [JSONPlaceholder - Users API](https://jsonplaceholder.typicode.com/users)
