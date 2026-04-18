  GoodsManager - Inventory Control System

  GoodsManager is a cross-platform .NET MAUI application designed for managing warehouse inventory. The system provides
  a multi-layered framework for tracking products, monitoring stock levels, and calculating total asset values across a
  persistent storage network.

  Key Features
   * Warehouse Dashboard: Displays a comprehensive list of registered warehouses, including geographical locations and
     aggregated stock values.
   * Detailed Inventory Management: Enables exploration of specific warehouse contents with automated total price
     calculations.
   * Product Specifications: Provides detailed data for each item, including category, quantity, unit price, and total
     batch value.
   * Responsive Asynchronous UX: Implements a global Busy State with visual overlays and activity indicators, ensuring
     the UI remains responsive during data operations.
   * Modern MVVM Implementation: Features real-time interface updates powered by CommunityToolkit.Mvvm, utilizing Source
     Generators for boilerplate-free property and command management.
   * Professional Data Mapping: Implements Data Transfer Objects (DTOs) to decouple the presentation layer from the
     internal data structures, ensuring a clean and secure data flow.

  Architecture and Tech Stack
  The project adheres to a strict 3-Layer Architecture (Presentation, Service, Data) to ensure maintainability and
  testability.

  Core Technologies
   * .NET 8.0 and MAUI: The primary framework for building cross-platform applications.
   * SQLite Persistence: Utilizes sqlite-net-pcl for local relational data storage, providing reliable data persistence
     between sessions.
   * Dependency Injection (DI): Services, Repositories, and ViewModels are managed via the .NET ServiceProvider,
     promoting loose coupling.
   * Shell Navigation: Uses hierarchical navigation with QueryProperty and automated data refreshing upon navigation.
   * Asynchronous Programming: Implements a full async/await lifecycle from the storage context up to the UI.

  Project Structure
   * GoodsManager: The main MAUI project containing Views, ViewModels, and UI Tools.
   * Common: Shared Enums and global constants.
   * DBModels: The data persistence layer models, decorated with SQLite attributes for relational mapping.
   * DTOModels: Data Transfer Objects used for safe communication between layers.
   * Services: The business logic layer. Handles mapping, calculations, and coordinates complex operations (like cascade
     deletes).
   * Repositories: Mediates between the storage context and business services using the Repository pattern.
   * Storage: Contains the persistent SqliteStorageContext and an optimized FileStorageContext with parallel data
     seeding.
   * Tools: UI helper classes, including EnumToDisplayNameConverter and visual layout components.
