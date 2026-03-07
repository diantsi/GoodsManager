# GoodsManager - Inventory Control System

**GoodsManager** is a cross-platform .NET MAUI application designed for managing warehouses inventory. The system provides a structured framework for tracking products, monitoring stock levels, and calculating total asset values within a distributed storage network.

## Key Features

* **Warehouse Dashboard**: Displays a comprehensive list of registered warehouses, including their geographical locations and aggregated stock values.
* **Detailed Inventory Management**: Enables exploration of specific warehouse contents with automated total price calculations for all stored items.
* **Product Specifications**: Provides detailed data for each item, including title, category, quantity, unit price, and total batch value.
* **Dynamic User Interface**: Implements real-time interface updates through the use of ObservableCollection and advanced data binding techniques.
* **Localized Metadata Display**: Features automatic translation of technical Enums (Cities and Categories) into user-readable text using custom IValueConverters.

## Architecture and Tech Stack

The project adheres to the **MVVM (Model-View-ViewModel)** architectural pattern to ensure strict separation between business logic and the presentation layer.



### Core Technologies
* **.NET 8.0 and MAUI**: The primary framework for building cross-platform mobile and desktop applications.
* **Dependency Injection (DI)**: Services and Pages are registered and managed via the built-in .NET ServiceProvider, promoting decoupled logic and testability.
* **Shell Navigation**: Uses a hierarchical navigation structure with QueryProperty for passing complex objects between views.
* **Reflection and Metadata**: Utilizes custom EnumExtensions and Reflection to extract display names from C# attributes for UI presentation.


## Project Structure

* **Common**: Contains shared Enums.
* **DBModels**: Represents the data persistence layer, containing classes that define the structure of the stored data.
* **Services**: The data access layer, featuring the IStorageService interface and its implementations (e.g., FakeStorage) for data retrieval and persistence.
* **UIModels**: Contains ViewModels that wrap raw database models (DbModels) to provide formatting, calculated properties, and data-binding logic for the UI.
* **Pages**: Consists of XAML views and corresponding code-behind logic for navigation and user interaction.
* **Tools**: Includes UI helper classes and converters, such as the EnumToDisplayNameConverter, to facilitate data transformation.
