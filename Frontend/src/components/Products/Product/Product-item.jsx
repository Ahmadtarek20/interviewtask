import React from "react";
import { domainName } from "../../../API";
const ProductItem = props => {
  const { id, imageUrl, name, price, companyName } = props.product;
  return (
    <tr>
      <td className="align-middle">
        <img
          src={domainName + "images/" + imageUrl}
          alt={name}
          title={name}
          className="img-fluid img-thumbnail"
          style={{ height: "50px", cursor: "pointer" }}
          onClick={() => props.ViewImageClick(id)}
        />
      </td>
      <td className="align-middle">{name}</td>
      <td className="align-middle">{price}</td>
      <td className="align-middle">{companyName}</td>
      <td className="align-middle">
        <button className="btn btn-primary" onClick={() => props.EditClick(id)}>
          <i className="fa fa-pencil" /> Edit
        </button>
      </td>
    </tr>
  );
};

export default ProductItem;
