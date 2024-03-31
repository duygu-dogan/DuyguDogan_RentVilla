import './App.css';
import '@coreui/coreui/dist/css/coreui.min.css'
import SidebarComponent from './components/SidebarComponent';
import HeaderComponent from './components/HeaderComponent';
import { BrowserRouter, Route, RouterProvider, Routes } from 'react-router-dom';
import NewProduct from './components/pages/Products/NewProduct';
import MainComponent from './components/MainComponent';
import '@fortawesome/fontawesome-free/css/all.min.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import 'react-toastify/dist/ReactToastify.css';

function App() {
    return (
        <>
            <div className='main-container'>
                <div className='sidebar-comp'>
                    <SidebarComponent />
                </div>
                <div className='header-comp'>
                    <HeaderComponent />
                </div>
                <div className='main-comp'>
                    <MainComponent />
                </div>
            </div>
        </>
    );
}

export default App;
