import React from "react";

import ProductItem from "./Product/Product-item";
const ProductList = props => {
  const { EditClick, ViewImageClick } = props;
  const tableHeadlines = [
    "Image",
    "Product Name",
    "Price",
    "Producer Company",
    "Action"
  ].map(headline => {
    return <th key={headline}>{headline}</th>;
  });

  const tableData = props.products.map(product => (
    <ProductItem
      key={product.id}
      product={product}
      EditClick={EditClick}
      ViewImageClick={ViewImageClick}
    />
  ));

  return (
    <table className="table table-hover">
      <thead className="thead-dark text-center">
        <tr>{tableHeadlines}</tr>
      </thead>

      <tbody className="text-center">{tableData}</tbody>
    </table>
  );
};

export default ProductList;
