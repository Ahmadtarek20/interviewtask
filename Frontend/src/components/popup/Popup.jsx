import React from "react";
import "./Popup.css";
const Popup = props => {
  return (
    <div className="pop-up">
      <div className="message">
        <span className="close btn btn-secondary" onClick={props.CloseClick}>
          x
        </span>
        <div className="message-body">{props.children}</div>
      </div>
    </div>
  );
};

export default Popup;
