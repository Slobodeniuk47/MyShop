import { useEffect, useState } from "react";
import { APP_ENV } from "../../../../env";
import { http } from "../../../../http";
import { IProductItem } from "../types";
import { Link, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { Modal } from "bootstrap"
import AdminNavbar from "../../../admin/adminHome/adminNavbar";

const AdminProductsView = () => {
    const [list, setList] = useState<IProductItem[]>([]);
    const [isLoading, setLoading] = useState<boolean>(true);
    const [deleteId, setId] = useState<number>();
    const navigator = useNavigate();
    const dispatch = useDispatch();

    useEffect(() => {
        loadProducts();
    }, []);

    const loadProducts = () => {
        http.get("api/product/get")
            .then(resp => {
                const data = resp.data.payload;
                
                setList(data);
                setLoading(false);
                console.log(data);
            });
    }

    const deleteConfirmed = () => {
        console.log("DeleteId", deleteId);

        http.delete('api/product/deleteProduct/' + deleteId)
            .then(() => {
                loadProducts();
            });
    }

    const deleteCategory = (id: number) => {
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
                            <h2>Products</h2>
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
                                        Name
                                    </th>
                                    {/* <th>
                                        Status
                                    </th> */}
                                    <th>
                                        Price
                                    </th>
                                    {/* <th>
                                        Discount Price
                                    </th> */}
                                    <th>
                                        Description
                                    </th>
                                    <th>
                                        CategoryId - Name
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>

                                {list ? (

                                    list.map((item: IProductItem) => (
                                        <tr key={item.id}>
                                            <td>
                                                <input type='checkbox' className='form-check-input'></input>
                                            </td>
                                            <td>
                                                {item.id}
                                            </td>
                                            <td>
                                                {item.images.length > 0 ? <img src={`${item.images[0].name}`} height={60}></img> : null}
                                                
                                            </td>
                                            <td>
                                                {item.name}
                                            </td>
                                            {/* <td>
                                                <text>{item.status}</text>
                                                {item.status == 1 ? <div className='ok'><span>Activated</span></div> : <div className='no'><span>Not working</span></div>}
                                            </td> */}
                                            <td>
                                                {item.price}
                                            </td>
                                            {/* <td>
                                                {item.discountPrice}
                                            </td> */}
                                            <td>
                                                {item.description}
                                            </td>
                                            <td>
                                                {item.categoryId} - {item.categoryName}
                                            </td>
                                            <td>
                                                <a onClick={() => deleteCategory(item.id)} ><i className='fa fa-trash btnDelete'></i></a>
                                                <Link to={"/admin/products/edit?id=" + item.id}><i className='fa fa-edit btnEdit'></i></Link>
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
export default AdminProductsView;