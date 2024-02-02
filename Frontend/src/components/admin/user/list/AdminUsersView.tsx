import { useEffect, useState } from "react";
import { APP_ENV } from "../../../../env";
import { http } from "../../../../http";
import { IUserItem } from "../types";
import { Link, useNavigate } from "react-router-dom";
import { useTypedSelector } from "../../../../store/hooks/useTypedSelector";
import { useDispatch, useSelector } from "react-redux";
import { Modal } from "bootstrap"
import AdminNavbar from "../../adminHome/adminNavbar";

const AdminUsersView = () => {
    const [list, setList] = useState<IUserItem[]>([]);
    const [isLoading, setLoading] = useState<boolean>(true);
    const [deleteId, setId] = useState<number>();
    const { isAuth, user } = useTypedSelector((store: any) => store.auth);
    const navigator = useNavigate();
    const dispatch = useDispatch();

    useEffect(() => {
        loadUsers();
    }, []);

    const loadUsers = () => {
        http.get("api/Account/get")
            .then(resp => {
                const { payload } = resp.data;
                console.log(payload);
                setList(payload);
                setLoading(false);
            });
    }

    const deleteConfirmed = () => {
        console.log(1);

        http.delete('api/Account/delete/' + deleteId)
            .then(() => {
                loadUsers();
            });
    }

    const deleteUser = (id: number) => {
        console.log(id);
        const myElement = document.getElementById("exampleModal") as HTMLElement;
        setId(id);
        const myModal = new Modal(myElement);
        myModal.show();
    }


    return (
        <div className="pageContent">
            <AdminNavbar/>
            {isLoading ? (
                <div className="loader-container">
                    <div className="spinner"></div>
                </div>
            ) : (
                <div className='pageList mt-5'>
                    <div className='ListColumn'>
                        <div className='tableHeader'>
                            <h2>Users</h2>
                            <Link to="create" className='btn btn-success'>
                                <i className='fa fa-2x fa-plus-circle'></i>
                                <span>Add</span>
                            </Link>
                        </div>
                        <table className="table listCategories">
                            <thead>
                                <tr>
                                    <th>
                                        <input type='checkbox' className='form-check-input allCheck'></input>
                                    </th>
                                    <th>
                                        Id
                                    </th>
                                    <th>
                                        Image
                                    </th>
                                    <th>
                                        FirstName
                                    </th>
                                    <th>
                                        LastName
                                    </th>
                                    <th>
                                        Email
                                    </th>
                                    <th>
                                        ImgName
                                    </th>
                                    <th>
                                        phoneNumber
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>

                                {list ? (

                                    list.map((item: IUserItem) => (
                                        <tr key={item.id}>
                                            <td>
                                                <input type='checkbox' className='form-check-input'></input>
                                            </td>
                                            <td>
                                                {item.id} [{ item.permissions[0] != null ? item.permissions[0].roleName : "None"}]
                                            </td>
                                            <td>
                                                <img src={APP_ENV.BASE_URL + 'Images/userImages/' + item.image} height={60}></img>
                                            </td>
                                            <td>
                                                {item.firstname}
                                            </td>
                                            <td>
                                                {item.lastname}
                                            </td>
                                            <td>
                                                {item.email}
                                            </td>
                                            <td>
                                                {item.image}
                                            </td>
                                            <td>
                                                {item.phoneNumber}
                                            </td>
                                            <td>
                                                <a onClick={() => deleteUser(item.id)} ><i className='fa fa-trash btnDelete'></i></a>
                                                
                                                <Link to={"/admin/users/edit?id=" + item.id}><i className='fa fa-edit btnEdit'></i></Link>
                                            </td>

                                        </tr>
                                    ))

                                )
                                    :
                                    null}

                            </tbody>
                        </table>

                    </div>
                    <div className="modal fade" id="exampleModal" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div className="modal-dialog">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title" id="exampleModalLabel">Confirm delete</h5>
                                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>

                                <div className="modal-footer">
                                    <button type="button" className="btn btn-secondary" data-bs-dismiss="modal" >Cancel</button>
                                    <button type="button" onClick={() => deleteConfirmed()} className="btn btn-primary" data-bs-dismiss="modal">Delete</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            )}
        </div>
    );
};
export default AdminUsersView;