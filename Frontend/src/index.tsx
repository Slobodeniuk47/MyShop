import React from 'react';
import ReactDOM from 'react-dom/client';
import { Provider } from 'react-redux';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import AdminCategoriesView from './components/admin/category/list/AdminCategoriesView';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import AdminCreateCategory from './components/admin/category/create/AdminCreateCategory';
import RegistrationView from './components/auth/registration/RegistrationPage';
import LoginView from './components/auth/login/LoginView';
import jwtDecode from "jwt-decode";
import { store } from "./store/store";
import { IUser } from "./components/auth/types";
import { http } from "./http";
import { AuthReducerActionType, IUserPayload } from './store/reducers/AuthReducer';
import AdminEditCategory from './components/admin/category/edit/AdminEditCategory';
import DashboardView from './components/dashboard/DashboardView';
import AdminCreateUser from './components/admin/user/create/AdminCreateUser';
import AdminProductsView from './components/admin/product/list/AdminProductsView';
import AdminCreateProduct from './components/admin/product/create/AdminCreateProduct';
import AdminEditProduct from './components/admin/product/edit/AdminEditProduct';
import HomePage from './components/home';
import AdminHomePage from './components/admin/adminHome';
import ProfilePage  from "./components/userProfile/index";
import ProductProfile from './components/home/productProfile/ProductProfile'
import AdminUsersView from './components/admin/user/list/AdminUsersView';
import AdminEditUser from './components/admin/user/edit/AdminEditUser';
import AdminViewRoles from './components/admin/role/AdminViewRoles';
import AdminEditRole from './components/admin/role/AdminEditRole';
import AdminCreateRole from './components/admin/role/AdminCreateRole';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

if (localStorage.token) {
  http.defaults.headers.common['Authorization'] = `Bearer ${localStorage.token}`;
  const user = jwtDecode(localStorage.token) as IUser;

  store.dispatch({
    type: AuthReducerActionType.LOGIN_USER,
    payload: IUserPayload(user)
  });
}

root.render(
  <Provider store={store}>
    <BrowserRouter>
      <Routes>
        <Route path="/admin" element={<App />}>
          <Route index element={<AdminHomePage/>} />
          <Route path="categories">
            <Route index element={<AdminCategoriesView />} />
            <Route path="create" element={<AdminCreateCategory />} />
            <Route path="edit" element={<AdminEditCategory />} />
          </Route>

          <Route path='products'>
            <Route index element={<AdminProductsView/>} />
            <Route path="create" element={<AdminCreateProduct />} />
            <Route path="edit" element={<AdminEditProduct />} />
          </Route>
          <Route path='users'>
            <Route index element={<AdminUsersView/>} />
            <Route path="create" element={<AdminCreateUser/>} />
            <Route path="edit" element={<AdminEditUser />} />
          </Route>
          <Route path='roles'>
            <Route index element={<AdminViewRoles/>} />
            <Route path="create" element={<AdminCreateRole/>} />
            <Route path="edit" element={<AdminEditRole />} />
          </Route>
        </Route>
        <Route path="/" element={<App />}>
          {/* <Route index element={<DashboardView />} /> */}
          <Route index element={<HomePage />} />
          <Route path="profile" element={<ProfilePage/>} />
          {/* <Route path='categories'>
            <Route index element={<CategoriesView />} />
            <Route path="create" element={<CreateCategory />} />
            <Route path="edit" element={<EditCategory />} />
          </Route> */}
          {/* <Route path='users'>
            <Route index element={<UsersView />} />
            <Route path="create" element={<CreateUser />} />
            <Route path="edit" element={<EditUser/>} />
          </Route> */}
          {/* <Route path='products'>
            <Route index element={<ProductsView/>} />
            <Route path="create" element={<CreateProduct />} />
            <Route path="edit" element={<EditProduct />} />
          </Route> */}

          <Route path="product" element={<ProductProfile />} />
          <Route path="register" element={<RegistrationView />} />
          <Route path="login" element={<LoginView />} />
        </Route>
      </Routes>
    </BrowserRouter>
  </Provider>

);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
