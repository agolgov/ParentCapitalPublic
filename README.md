# ParentCapitalBlazor
Blazor project for parent capital certificates
Описание процесса


=======В razor:
@if (_certificateList == null)
{
    <p>Загрузка сертификатов...</p>
    <div class="spinner"></div>
}
else
{}
=======В css:
.spinner {
    border: 16px solid silver;
    border-top: 16px solid #337AB7;
    border-radius: 50%;
    width: 80px;
    height: 80px;
    animation: spin 700ms linear infinite;
    top: 40%;
    left: 55%;
    position: absolute;
}
@keyframes spin {
    0% {
        transform: rotate(0deg);
    }

    100% {
        transform: rotate(360deg);
    }
}
======Напоминание
System.Threading.Thread.Sleep(3000);