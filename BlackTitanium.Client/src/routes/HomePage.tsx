import {Card, CardContent, CardFooter} from "@shadcn/components/ui/card.tsx";
import {PersonIcon} from "@radix-ui/react-icons";
import {useNavigate} from "react-router-dom";

export default function HomePage() {
    const navigate = useNavigate();
    return (
        <div className="flex flex-row space-x-4 justify-center self-center">
            <Card onClick={() => navigate("/attendance/personal")}
                  className="w-64 bg-card text-card-foreground cursor-pointer hover:bg-card-foreground hover:text-card">
                <CardContent className="flex justify-center items-center">
                    <PersonIcon className="h-3/4 w-3/4"/>
                </CardContent>
                <CardFooter className="justify-center">
                    <h2 className="text-center text-lg">Личное посещение</h2>
                </CardFooter>
            </Card>
            <Card onClick={() => navigate("/attendance/group")}
                  className="w-64 bg-card text-card-foreground cursor-pointer hover:bg-card-foreground hover:text-card">
                <CardContent className="flex justify-center items-center">
                    <PersonIcon className="h-3/4 w-3/4"/>
                </CardContent>
                <CardFooter className="justify-center">
                    <h2 className="text-center text-lg">Групповое посещение</h2>
                </CardFooter>
            </Card>
        </div>
    )
}