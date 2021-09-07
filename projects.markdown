---
layout: default
title: Projects
permalink: /projects/
---

### Links

- [Home](/)
- [About me](/about)
- [Projects](/projects)

## Board Games Rental Application

![index page](/images/boardgames-index-page.png)

Web application created with ASP.NET MVC, Entity Framework (Code First approach), MySql database and Unit of Work Pattern.

This application was developed from the last months of 2019 to January 2020 by [me](https://github.com/APB97) and few other students.

To run local database, we used xampp.

My main responsibility was to write and debug C# backend of the website, in particular:
- provide password salting and hashing for secure storage of sensitive data

- provide user login logic
	- hide login/register links when user is already logged in
	- provide logout option
	- redirect already logged in users to home page when trying to access login page

- provide board game category management
- provide board game management
- provide ellipsis service to shorten long descriptions
- provide board game publisher management
- provide recommended board games

