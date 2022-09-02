function convertFirstLetterToUpperCase(text) {
    return text.charAt(0).toUpperCase() + text.slice(1);
//charAt fonksiyonu vereceğimiz index'teki harfi almamızı sağlar. 0 ile ilk harfi alır.
    //slice metodu ise bir index ister ve string'in oradan alınmasını sağlar.
}
function convertToShortDate(dateString) {
    const shortDate = new Date(dateString).toLocaleDateString('en-US');
    return shortDate;
}