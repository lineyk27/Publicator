
export default function reprDate(datestr){
    let date = new Date(datestr);
    return `${date.getFullYear()}-${date.getMonth()}-${date.getDate()}`;
}
