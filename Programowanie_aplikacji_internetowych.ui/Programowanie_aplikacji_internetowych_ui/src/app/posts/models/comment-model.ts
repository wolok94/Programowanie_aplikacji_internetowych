import { MetaDataModel } from "../../shared/models/metadata-model";

export interface Comment{
    id: number;         
    text: string;        
    metaData: MetaDataModel;  
}