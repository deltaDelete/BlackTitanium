import React from 'react'
import ReactDOM from 'react-dom/client'
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import App from './App.tsx'
import './index.css'
import './global.css'
import HomePage from "@/routes/HomePage.tsx";
import PersonalAttendancePage from "@/routes/PersonalAttendancePage.tsx";
import GroupAttendancePage from "@/routes/GroupAttendancePage.tsx";
import ErrorPage from "@/routes/ErrorPage.tsx";

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        errorElement: <ErrorPage />,
        children: [
            {
                path: "/",
                element: <HomePage />,
            },
            {
                path: "/attendance/personal",
                element: <PersonalAttendancePage />
            },
            {
                path: "/attendance/group",
                element: <GroupAttendancePage />
            }
        ]
    },
]);

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <RouterProvider router={router}/>
  </React.StrictMode>,
)
