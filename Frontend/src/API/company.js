import { axiosBase } from "./index";
export function getCompanies() {
  return axiosBase.get(`/companies`).then(res => res.data);
}
