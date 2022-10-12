<template>
  <div id="app" >

    <div class="d-flex flex-column flex-md-row align-items-center justify-content-between p-1 px-mb-4 
    mb-3 bg-white border-bottom shadow-sm fixed-top"> 
    <!-- <a href="/" class="d-flex align-items-center text-dark text-decoration-none">
      <svg xmlns="http://www.w3.org/2000/svg" width="40" height="32" class="me-2" viewBox="0 0 118 94" role="img"><title>Bootstrap</title><path fill-rule="evenodd" clip-rule="evenodd" d="M24.509 0c-6.733 0-11.715 5.893-11.492 12.284.214 6.14-.064 14.092-2.066 20.577C8.943 39.365 5.547 43.485 0 44.014v5.972c5.547.529 8.943 4.649 10.951 11.153 2.002 6.485 2.28 14.437 2.066 20.577C12.794 88.106 17.776 94 24.51 94H93.5c6.733 0 11.714-5.893 11.491-12.284-.214-6.14.064-14.092 2.066-20.577 2.009-6.504 5.396-10.624 10.943-11.153v-5.972c-5.547-.529-8.934-4.649-10.943-11.153-2.002-6.484-2.28-14.437-2.066-20.577C105.214 5.894 100.233 0 93.5 0H24.508zM80 57.863C80 66.663 73.436 72 62.543 72H44a2 2 0 01-2-2V24a2 2 0 012-2h18.437c9.083 0 15.044 4.92 15.044 12.474 0 5.302-4.01 10.049-9.119 10.88v.277C75.317 46.394 80 51.21 80 57.863zM60.521 28.34H49.948v14.934h8.905c6.884 0 10.68-2.772 10.68-7.727 0-4.643-3.264-7.207-9.012-7.207zM49.948 49.2v16.458H60.91c7.167 0 10.964-2.876 10.964-8.281 0-5.406-3.903-8.178-11.425-8.178H49.948z" fill="currentColor"></path></svg>
      <span class="fs-4">Pricing example</span>
    </a> -->
    <img src="./assets/images/logo.jpg" class="mt-2" style="max-height: 45px;" alt="logo">

    <nav class="router-link-exact-active my-2 my-md-0 mr-md-3" style="font-size: larger;">
        <!-- <a class="me-3 py-2 text-dark text-decoration-none" href="#">الإدارة</a>
        <a class="me-3 py-2 text-dark text-decoration-none" href="#">القرعة</a>
        <a class="me-3 py-2 text-dark text-decoration-none" href="#">تاريخ القِرع</a> -->
        
        <router-link class="me-3 py-2  text-decoration-none"  to="/admin">إدارة</router-link> |
        <router-link class="me-3 py-2 text-decoration-none" to="/about">تاريخ القِرع</router-link> |
        <router-link class="me-3 py-2  text-decoration-none" to="/">القرعة</router-link> 
    </nav>
    
     <div class="d-flex align-item-center">
        <!-- <a>
            <i class="fa fa-solid fa-phone fa-2x mr-5 text-dark"></i> 
        </a> 
         <a class="btn btn-outline-secondary">تحسين</a> -->
    </div>
  </div>
    <!-- <nav>
      <router-link to="/">Home</router-link> |
      <router-link to="/about">About</router-link> |
      <router-link to="/admin">admin</router-link>
    </nav> -->
    <router-view class="body-cont"/>
    <div class="container-fo sticky-xxl-bottom">
  <footer class="py-3 my-4 ">
    <ul class="nav justify-content-center border-bottom pb-3 mb-3">
      <li class="nav-item"><a href="#" class="nav-link px-2 text-muted">Home</a></li>
      <li class="nav-item"><a href="#" class="nav-link px-2 text-muted">Features</a></li>
      <li class="nav-item"><a href="#" class="nav-link px-2 text-muted">Pricing</a></li>
      <li class="nav-item"><a href="#" class="nav-link px-2 text-muted">FAQs</a></li>
      <li class="nav-item"><a href="#" class="nav-link px-2 text-muted">About</a></li>
    </ul>
    <p class="text-center text-muted">© 2022 Company, Inc</p>
  </footer>
</div>
  </div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

/* nav {
  padding: 30px;
} */

nav a {
  font-weight: bold;
  color: #2c3e50;
} 

nav .router-link-exact-active {
  color: #2c3e50 !important;
  text-decoration: underline !important;
}

.body-cont{
  position: absolute;
    top: 35%;
    right: 10%;
    left: 10%;
}
  .container-fo {
    position: absolute;
    right: 2%;
    left: 2%;
    bottom: 0;
    margin: 0 !important;
    text-align: center;
  
}
</style>
