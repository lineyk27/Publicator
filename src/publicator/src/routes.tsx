enum HotPeriod{
    Day,
    Week,
    Month,
    Year
}

export default {
    home:"/home",
    posts: {
        new: "/new",
        hot: (period: HotPeriod) => `/hot?period=${period.toString()}`,
        hotTemplate: "/hot?period=:period",
        bySubscription: "/postsBySubscriptions",
        byId: (id: string) => `/posts/${id}`,
        byIdTemplate: "/posts/:id",
        createNew: "/createNew",
        bookmarks: "/bookmarks"
    },
    users: {
        myProfile: "/me",
        userByUsername: (username: string) => `/user/${username}`,
        userByUsernameTemplate: "/user/:username"
    },
    auth: {
        login: "/login",
        facebookLogin: "/facebook-login",
        register: "/register"
    }
}