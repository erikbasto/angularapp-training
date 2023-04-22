export interface Group{
    namne:string;
    connections:Connection[];
}

export interface Connection{
    connectionId:string;
    username:string;
}
