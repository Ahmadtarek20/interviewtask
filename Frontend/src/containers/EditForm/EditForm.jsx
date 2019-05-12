import React, { Component } from "react";
import "./EditForm.css";
import { domainName } from "../../API";
import { editProduct } from "../../API/product";
class EditForm extends Component {
  state = {
    formData: {
      photo: {
        value: ""
      },
      name: {
        value: ""
      },
      price: {
        value: ""
      },
      companyname: {
        value: ""
      }
    }
  };

  async componentDidMount() {
    const product = await editProduct(this.props.product.id);
    console.log(product);
    delete product.id;
    const obj = {
      formData: {}
    };
    for (let key in product) {
      obj.formData[key] = {
        value: product[key]
      };
    }
    this.setState(obj);
  }

  onSubmitHandler = async () => {
    this.setState({
      ...this.state
    });

    let product = {};

    for (let key in this.state.formData) {
      product[key] = this.state.formData[key].value;
    }
    await editProduct(this.props.product.id, product);
  };

  render() {
    const { id, name, price, imageUrl } = this.props.product;

    return (
      <form>
        <input
          type="hidden"
          name="image"
          id="image"
          className="form-control"
          value={imageUrl}
        />
        <input type="hidden" name="id" className="form-control" value={id} />
        <div className="form-group text-center">
          <div
            className="product-image mx-auto"
            style={{
              backgroundImage: `url(${domainName}images/${imageUrl})`
            }}
            onClick={this.imageClickHandler}
          />
          <input type="file" name="image" id="image" className="form-control" />
        </div>

        <div className="form-group">
          <label htmlFor="productname">Product Name :</label>
          <input
            type="text"
            name="name"
            id="productname"
            className="form-control"
            placeholder="Type The Product Name"
            defaultValue={name}
          />
        </div>

        <div className="form-group">
          <label htmlFor="productprice">Product Price :</label>
          <input
            type="text"
            name="price"
            id="productprice"
            className="form-control"
            placeholder="Type The Product Price (EGP)"
            defaultValue={price}
          />
        </div>

        <div className="form-group">
          <label htmlFor="company">Select Company Producer :</label>
          <select name="companyname" id="company" className="form-control">
            {this.props.companies.map((company, index) => (
              <option value={company.name} key={index}>
                {company.name}
              </option>
            ))}
          </select>
        </div>

        <div className="form-group d-flex justify-content-end">
          <div className="">
            <input type="reset" className="btn btn-outline-danger mx-1" />
            <input type="submit" className="btn btn-info mx-1" value="Save" />
          </div>
        </div>
      </form>
    );
  }
}
export default EditForm;
