import {useDispatch} from 'react-redux';
import {AppDispatch} from '../store';

//Use throughout in your app instead of plain `useDispatch` 
export const useTypedDispatch = () => useDispatch<AppDispatch>();