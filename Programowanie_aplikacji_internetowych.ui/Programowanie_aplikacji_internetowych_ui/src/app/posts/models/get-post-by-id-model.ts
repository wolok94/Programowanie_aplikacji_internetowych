import { MetaDataModel } from "../../shared/models/metadata-model";
import { Comment } from "./comment-model";

export interface GetPostByIdModel{
    id: string;             
    title: string;          
    text: string;           
    imageUrl: string;        
    comments: Comment[];    
    metaData: MetaDataModel;     
}