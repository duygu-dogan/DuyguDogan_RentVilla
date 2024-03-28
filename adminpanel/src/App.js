import './App.css';
import '@coreui/coreui/dist/css/coreui.min.css'
import SidebarComponent from './components/SidebarComponent';
import HeaderComponent from './components/HeaderComponent';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import NewProduct from './components/pages/Products/NewProduct';
import MainComponent from './components/MainComponent';
import '@fortawesome/fontawesome-free/css/all.min.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import 'react-toastify/dist/ReactToastify.css';
import ListProducts from './components/pages/Products/ListProducts';
import DeletedAttributeTable from './components/helpers/DeletedAttributeTable';
import ListAttributes from './components/pages/Products/ListAttributes';
import ListAttributeTypes from './components/pages/Products/ListAttributeTypes';
import DeletedProductTable from './components/helpers/DeletedProductTable';
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
              <Route path='/products' element={<ListProducts />} />
              <Route path='/attributetypes' element={<ListAttributeTypes />} />
              <Route path='/deletedattributetypes' element={<DeletedAttributeTable />} />
              <Route path='/attributes/:id' element={<ListAttributes />} />
              <Route path='/deletedproducts' element={<DeletedProductTable />} />
            </Routes>
          </div>
        </div>
      </BrowserRouter>
    </>
  );
}

export default App;
