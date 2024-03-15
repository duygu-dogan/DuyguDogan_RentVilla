import './App.css';
import SidebarComponent from './components/SidebarComponent'
import FooterComponent from './components/FooterComponent'
import HeaderComponent from './components/HeaderComponent'
import ContentComponent from './components/ContentComponent'

function App() {
  return (
    <>
      <div>
        <SidebarComponent />
        <div className="wrapper d-flex flex-column min-vh-100">
          <HeaderComponent />
          <div className="body flex-grow-1">
            <ContentComponent />
          </div>
          <FooterComponent />
        </div>
      </div>
    </>
  );
}

export default App;
