# StudentForm Application

A web application built for students to fill out and manage forms easily. The application provides a platform for students to submit their forms, and it supports downloading PDFs of the filled-out forms.

## Features

- **Form Submission**: Students can fill out and submit forms.
- **PDF Generation**: The forms submitted by students can be downloaded as PDFs.
- **Authentication**: (Optional) You can implement authentication to ensure that only authorized users can submit forms.
- **Responsive Design**: The app is fully responsive and works seamlessly on both desktop and mobile devices.

## Technologies Used

- **Frontend**:
  - React.js: A JavaScript library for building user interfaces.
  - Nginx: A web server used to serve the React application.
  
- **Backend**:
  - ASP.NET Core: A framework used for building the API that handles form submissions and generates PDFs.
  - C#: The programming language used for building the backend logic.
  - Entity Framework Core: ORM for interacting with the database.
  - JWT Authentication: Used for securing API endpoints (if authentication is required).
  
- **Database**:
  - (Optional) SQL Server: A relational database used for storing student form data.

## Setup Instructions

### Prerequisites

Before running the application, ensure you have the following installed:

- Docker: https://www.docker.com/get-started
- .NET SDK (for backend)
- Node.js (for frontend)

### Running the Application with Docker

1. Clone the repository:

    ```bash
    git clone <repository-url>
    ```

2. Navigate to the project directory:

    ```bash
    cd StudentFormApp
    ```

3. Build and run the application using Docker:

    ```bash
    docker-compose up --build
    ```

    This will:
    - Build both frontend and backend Docker containers.
    - Start the application with the necessary services (Frontend, Backend).

### Accessing the Application

- **Frontend**: Open a browser and navigate to [http://localhost:3000](http://localhost:3000) to access the React frontend.
- **Backend**: The backend API is available at [http://localhost:5042](http://localhost:5042).

### Stopping the Application

To stop the running containers, use:

```bash
docker-compose down
