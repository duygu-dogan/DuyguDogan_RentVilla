import './App.css';
import '@coreui/coreui/dist/css/coreui.min.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import SidebarComponent from './components/SidebarComponent';
import HeaderComponent from './components/HeaderComponent';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import NewProduct from './components/pages/NewProduct';
import MainComponent from './components/MainComponent';
function App() {
  return (
    <>
      <BrowserRouter>
        <div className='main-container'>
          <div className='sidebar-comp'>
            <SidebarComponent />
          </div>
          <div className='header-comp'>
            <HeaderComponent />
          </div>
          <div className='main-comp'>
            <Routes>
              <Route path='/' element={<MainComponent />} />
              <Route path='/newproduct' element={<NewProduct />} />
            </Routes>
          </div>
        </div>
      </BrowserRouter>
    </>
  );
}

export default App;
