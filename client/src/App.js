import React from "react";

import EmailConfirm from "./Components/EmailPages/EmailConfirm";
import EmailRecover from "./Components/EmailPages/EmailRecover";
import EmailVerification from "./Components/EmailPages/EmailVerification";
import EmailRemoveRequest from "./Components/EmailPages/EmailRemoveRequest";
import HomePage from "./Components/Home/HomePage";
import SuccessSubmitMessage from "./Components/Info/SuccessSubmitMessage";
import ContactPage from "./Components/Info/ContactPage";
import ErrorInfo from "./Components/Info/ErrorInfo";
import { BrowserRouter, Route } from "react-router-dom";

function App() {
  return (
    <div>
      <BrowserRouter>
        <Route path="/" exact component={HomePage} />
        <Route path="/verify" component={EmailVerification} />
        <Route path="/confirm" component={EmailConfirm} />
        <Route path="/recover" component={EmailRecover} />
        <Route path="/delete" component={EmailRemoveRequest} />
        <Route path="/delete" component={EmailRemoveRequest} />
        <Route path="/contact" component={ContactPage} />
        <Route path="/success" component={SuccessSubmitMessage} />
        <Route path="/error" component={ErrorInfo} />
      </BrowserRouter>
    </div>
  );
}

export default App;
