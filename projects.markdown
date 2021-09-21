---
layout: default
title: Projects
permalink: /projects/
---

### Links

- [Home](/)
- [About me](/about)
- [Projects](/projects)

## Plot and Integrate

![Plot made larger](https://github.com/APB97/PlotAndIntegrate/raw/main/images/plot-made-larger.png)

[Repository link](https://github.com/APB97/PlotAndIntegrate)

<p align="justify">
Windows Forms application that can be used for drawing simple plots and calculating definite integrals.
</p>

<p align="justify">
Displayed plot can be enlarged to take the whole available form space moving all options controls to the right.
Application allows saving plot as PNG (Portable Network Graphics) image, regardless of its size on-screen.
Apart from that, users can see coordinates under the cursor position, current function's formula, change unit of the plot by scrolling with a mouse wheel or setting a desired value.
Users can pan the plot by holding a mouse button and moving their cursor at the same time.
Plot can be drawn with various pen sizes and colors and font of axes' labels can be changed.
</p>

<p align="justify">
The application currently supports polynomial and logarithmic functions for drawing and integral calculations.
Calculating integral requires to pass desired range into <code>from:</code> and <code>to:</code> boxes.
</p>

## Board Games Rental Application

![index page](/images/boardgames-index-page.png)

[Repository link](https://github.com/APB97/BoardGamesRentalApplication)

<p align="justify">
Web application created with ASP.NET MVC, Entity Framework (Code First approach), MySql database and Unit of Work Pattern.
</p>

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

