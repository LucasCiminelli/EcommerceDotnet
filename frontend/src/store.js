import { configureStore } from "@reduxjs/toolkit";
import { productsReducer } from "./slices/productsSlice";
import { productByIdReducer } from "./slices/productByIdSlice";
import { productsPaginationReducer } from "./slices/productsPaginationSlice";

export default configureStore({
  reducer: {
    products: productsReducer,
    product: productByIdReducer,
    productsPagination: productsPaginationReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({ serializableCheck: false }),
});
