import './App.css';
import '@coreui/coreui/dist/css/coreui.min.css'
import SidebarComponent from './components/SidebarComponent';
import HeaderComponent from './components/HeaderComponent';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import NewProduct from './components/pages/Products/NewProduct';
import MainComponent from './components/MainComponent';
import '@fortawesome/fontawesome-free/css/all.min.css';
// import 'mdbreact/dist/css/mdb.css';
import 'bootstrap/dist/css/bootstrap.min.css'

import ListProducts from './components/pages/Products/ListProducts';
import Attributes from './components/pages/Products/Attributes';
import NewAttribute from './components/pages/Products/NewAttribute';
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
              <Route path='/getproducts' element={<ListProducts />} />
              <Route path='/getattributes' element={<Attributes />} />
              <Route path='/newattribute' element={<NewAttribute />} />
            </Routes>
          </div>
        </div>
      </BrowserRouter>
    </>
  );
}

export default App;
