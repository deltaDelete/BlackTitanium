import './App.css'
import './global.css'
import Header from "@/components/header/Header.tsx";
import {Outlet} from "react-router-dom";

function App() {
    return (
        <div className="flex flex-col space-y-4 select-none">
            <Header/>
            <div>
                <Outlet/>
            </div>
            <footer className="read-the-docs">
                ПОДВАЛ
            </footer>
        </div>
    )
}

export default App
