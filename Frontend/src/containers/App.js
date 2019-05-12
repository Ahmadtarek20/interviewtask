import React, { Component } from "react";

import "./App.css";
import ProductList from "../components/Products/product-list";
import Popup from "../components/popup/Popup";
import EditForm from "./EditForm/EditForm";
import ErrorBoundary from "../components/ErrorBoundary";
import { domainName } from "../API/index";
import { getProducts } from "../API/product";
import { getCompanies } from "../API/company";
// import { products } from "../data";
class App extends Component {
  constructor() {
    super();
    this.state = {
      products: [],
      companies: [],
      selectedProduct: 0,
      isOpen: false,
      viewImage: false
    };
  }

  async componentDidMount() {
    const products = await getProducts();
    const companies = await getCompanies();
    this.setState({
      products,
      companies
    });
    console.log(this.state.companies);
  }

  onEditClickHandler = id => {
    const products = [...this.state.products];
    const productIndex = products.findIndex(product => product.id === id);
    this.setState({ isOpen: true, selectedProduct: productIndex });
  };

  onViewImageClickHandler = id => {
    const products = [...this.state.products];
    const productIndex = products.findIndex(product => product.id === id);
    this.setState({
      isOpen: true,
      viewImage: true,
      selectedProduct: productIndex
    });
  };

  onCloseClickHandler = () => {
    this.setState({ isOpen: false, viewImage: false });
  };
  render() {
    const SelectedProduct = this.state.products[this.state.selectedProduct];
    return (
      <div className="container">
        <ErrorBoundary>
          <ProductList
            products={this.state.products}
            EditClick={this.onEditClickHandler}
            ViewImageClick={this.onViewImageClickHandler}
          />
        </ErrorBoundary>

        {this.state.isOpen && (
          <Popup CloseClick={this.onCloseClickHandler}>
            {this.state.viewImage ? (
              <img
                src={domainName + "images/" + SelectedProduct.imageUrl}
                alt={SelectedProduct.name}
                title={SelectedProduct.name}
                className="img-fluid"
              />
            ) : (
              <ErrorBoundary>
                <EditForm
                  product={SelectedProduct}
                  companies={this.state.companies}
                />
              </ErrorBoundary>
            )}
          </Popup>
        )}
      </div>
    );
  }
}

export default App;
