import Result from "./components/Result.vue";
import Home from "./components/Home.vue";

const routes = [
  {
    name: "home",
    path: "/",
    component: Home,
  },
  {
    name: "result",
    path: "/result/:id/:fileName",
    component: Result
  }
];

export default routes;
