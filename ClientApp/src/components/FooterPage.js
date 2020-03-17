import React from "react";
import { MDBContainer, MDBFooter } from "mdbreact";

const FooterPage = () => {
  return (
    <MDBFooter color="blue" className="font-small pt-4 mt-4" style={{position:"absolute",left: 0,bottom: 0,width: '100%',overflow: 'hidden'}}>
      <div className="footer-copyright text-center py-3">
        <MDBContainer fluid>
          &copy; 2020 Built By <a href="https://seif.rocks">Seif Elmughrabi</a>  
        </MDBContainer>
        <MDBContainer fluid>
         <a href="https://seif.rocks">Terms (please read)</a> <br/> <a href="https://seif.rocks">Privacy</a> 
        </MDBContainer>
      </div>
    </MDBFooter>
  );
}

export default FooterPage;