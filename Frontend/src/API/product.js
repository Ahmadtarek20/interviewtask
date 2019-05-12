import { axiosBase } from "./index";

export function getProducts() {
  return axiosBase.get(`/products`).then(res => res.data);
}

export function editProduct(id, product) {
  return axiosBase.put(`products/${id}`, product).then(res => res.data);
}
