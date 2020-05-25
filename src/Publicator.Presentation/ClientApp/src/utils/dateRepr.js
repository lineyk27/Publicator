
export default function reprDate(datestr){
    let date = new Date(datestr);
    return `${date.getDate()}/${date.getMonth()}/${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}`;
}
