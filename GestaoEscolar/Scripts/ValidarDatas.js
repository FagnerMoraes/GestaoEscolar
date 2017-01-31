function checarDatas(data1, data2) {

    if (!data1 || !data2)
        return false;
    if (data1 > data2) {
        alert("Data não pode ser maior que a data final");
        return false;
    } else {
        return true;
    }
}