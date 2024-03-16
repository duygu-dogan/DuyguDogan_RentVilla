import './App.css';
import '@coreui/coreui/dist/css/coreui.min.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import SidebarComponent from './components/SidebarComponent';
import HeaderComponent from './components/HeaderComponent';
import { CNavbarToggler } from '@coreui/react';
import { useState } from 'react';

function App() {
  return (
    <>
      <div className='d-flex row'>
        <div className='col-2'>
          <SidebarComponent />
        </div>
        <div className='col-10'>
          <HeaderComponent />
        </div>

      </div>
    </>
  );
}

export default App;
