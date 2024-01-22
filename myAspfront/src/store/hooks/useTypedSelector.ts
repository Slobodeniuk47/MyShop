import {TypedUseSelectorHook, useSelector} from "react-redux";
import {RootState} from "../store";

//Use throughout in your app instead of plain `useSelector`
export const useTypedSelector : TypedUseSelectorHook<RootState> = useSelector;