import classNames from "classnames";
import { useState } from "react";
import car1 from "../../assets/1.jpg";
import car2 from "../../assets/2.jpg";
import car3 from "../../assets/3.jpg";
import { APP_ENV } from "../../env";
import { IProductImageItem } from "../admin/product/types";

interface ICarusel {
  img: string;
}
const str1 = { car1, car2, car3 }
const str2 = [{ car1 }, { car2 }, { car3 }]
var productImages;
const Slider = (images: any) => {
  productImages = images;
  const [items, setItems] = useState<ICarusel[]>([
    { img: car1 },
    { img: car2 },
    { img: car3 },
  ]);


  // console.log("cars");

  return (
    <>
      {productImages ? (
                <div className="text-center mt-5">
                    <div>{productImages.length >= 0 ? <img src={`${APP_ENV.BASE_URL}Images/productImages/${productImages[0].name}`} height={60}></img> : null}</div>

                </div>
            ) : "null"}
      <div
        id="carouselExampleControls"
        className="carousel slide"
        data-bs-ride="carousel"
      >
        <div className="carousel-inner">
          {items.map((item, index) => (
            <div key={index}
              className={classNames("carousel-item", {"active": index===0})}
            >
              <img src={item.img} className="d-block w-100" alt="sliderImage" />
            </div>
          ))}
        </div>
        <button
          className="carousel-control-prev"
          type="button"
          data-bs-target="#carouselExampleControls"
          data-bs-slide="prev"
        >
          <span
            className="carousel-control-prev-icon"
            aria-hidden="true"
          ></span>
          {/* <span className="visually-hidden">Previous</span> */}
        </button>
        <button
          className="carousel-control-next"
          type="button"
          data-bs-target="#carouselExampleControls"
          data-bs-slide="next"
        >
          <span
            className="carousel-control-next-icon"
            aria-hidden="true"
          ></span>
          {/* <span className="visually-hidden">Next</span> */}
        </button>
      </div>
    </>
  );
};

export default Slider;
