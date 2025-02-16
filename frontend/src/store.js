import { configureStore } from "@reduxjs/toolkit";
import { productsReducer } from "./slices/productsSlice";
import { productByIdReducer } from "./slices/productByIdSlice";
import { productsPaginationReducer } from "./slices/productsPaginationSlice";
import { categoriesReducer } from "./slices/categorySlice";

export default configureStore({
  reducer: {
    products: productsReducer,
    product: productByIdReducer,
    productsPagination: productsPaginationReducer,
    categories: categoriesReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({ serializableCheck: false }),
});
