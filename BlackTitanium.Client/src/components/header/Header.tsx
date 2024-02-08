import {Avatar, AvatarFallback, AvatarImage} from "@shadcn/components/ui/avatar.tsx";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem, DropdownMenuLabel,
    DropdownMenuTrigger
} from "@shadcn/components/ui/dropdown-menu.tsx";
import {useNavigate} from "react-router-dom";

function Header() {
    const nav = useNavigate();
    return (
        <header id="header"
                className="h-16 bg-accent text-accent-foreground mx-0 py-0 px-4 flex flex-row items-center justify-between">
            <div className="flex flex-row space-x-4 items-center cursor-pointer" onClick={() => nav("/")}>
                <Avatar>
                    <AvatarImage src="" />
                    <AvatarFallback>LO</AvatarFallback>
                </Avatar>
                <h1>Logo</h1>
            </div>
            <h1 id="Title" className="items-center flex-grow absolute left-1/2 transform -translate-x-1/2"></h1>
            <DropdownMenu>
                <DropdownMenuTrigger >
                    <Avatar>
                        <AvatarFallback>АН</AvatarFallback>
                    </Avatar>
                </DropdownMenuTrigger>
                <DropdownMenuContent>
                    <DropdownMenuLabel>
                        Мой профиль
                    </DropdownMenuLabel>
                    <DropdownMenuItem>Войти</DropdownMenuItem>
                    <DropdownMenuItem>Зарегистрироваться</DropdownMenuItem>
                </DropdownMenuContent>
            </DropdownMenu>
        </header>
    )
}

export default Header;