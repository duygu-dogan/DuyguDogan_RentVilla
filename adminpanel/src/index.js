import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import '@coreui/coreui/dist/css/coreui.min.css';
import '@fortawesome/fontawesome-free/css/all.min.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import 'react-toastify/dist/ReactToastify.css';
import { Outlet, RouterProvider, createBrowserRouter } from 'react-router-dom';
import ListProducts from './components/pages/Products/ListProducts';
import NewProduct from './components/pages/Products/NewProduct';
import ListAttributeTypes from './components/pages/Products/ListAttributeTypes';
import ListAttributes from './components/pages/Products/ListAttributes';
import EditProduct from './components/pages/Products/EditProducts';
import DeletedAttributeTable from './components/helpers/DeletedAttributeTable';
import DeletedProductTable from './components/helpers/DeletedProductTable';
import SidebarComponent from './components/SidebarComponent';
import HeaderComponent from './components/HeaderComponent';
import MainComponent from './components/MainComponent';
import ListStateImage from './components/pages/Settings/Region/ListStateImage';

function Layout() {
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
          <Outlet />

        </div>
      </div>
    </>)
}
const router = createBrowserRouter([
  {
    path: '/admin',
    element: <Layout />,
    children: [
      { path: '', element: <MainComponent /> },
      { path: 'products', element: <ListProducts /> },
      { path: 'newproduct', element: <NewProduct /> },
      { path: 'attributetypes', element: <ListAttributeTypes /> },
      { path: 'attributes/:id', element: <ListAttributes /> },
      { path: 'editproduct/:id', element: <EditProduct /> },
      { path: 'deletedattributetypes', element: <DeletedAttributeTable /> },
      { path: 'deletedproducts', element: <DeletedProductTable /> },
      { path: 'states', element: <ListStateImage /> }
    ]
  }
])
ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <RouterProvider
      router={router}
    />
  </React.StrictMode>
);
