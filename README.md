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

Before running the application, make sure the following tools are installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (for backend)
- [Node.js](https://nodejs.org/en/download) (for frontend)
- (Optional) [Docker](https://www.docker.com/get-started) – to run SQL Server locally

---

### Running the Application Locally

You can run the frontend and backend separately on your local machine. Optionally, you can also run a SQL Server database using Docker for full data persistence.

---

#### 1. Run the Backend (ASP.NET Core)

1. Navigate to the backend directory:

    ```bash
    cd backend
    ```

2. Update the connection string in `appsettings.Development.json`:
   - If you’re not using a database, you can skip this step or use an in-memory setup (if implemented).
   - If using SQL Server, configure the connection as shown in the "Optional: Add SQL Server" section below.

3. Start the backend:

    ```bash
    dotnet run
    ```

4. The backend API will be available at:

    ```
    http://localhost:5042
    ```

---

#### 2. Run the Frontend (React)

1. Open a new terminal and navigate to the frontend directory:

    ```bash
    cd frontend
    ```

2. Install the dependencies:

    ```bash
    npm install
    ```

3. Start the development server:

    ```bash
    npm start
    ```

4. Open the application in your browser:

    ```
    http://localhost:3000
    ```

