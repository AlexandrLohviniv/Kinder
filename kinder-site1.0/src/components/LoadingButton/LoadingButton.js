import React from 'react';
import {Button} from 'react-bootstrap';


const LoadingButton = ({text}) => {
    // function simulateNetworkRequest() {
    //     return new Promise((resolve) => setTimeout(resolve, 2000));
    //   }
      
    //   function LoadingButton() {
    //     const [isLoading, setLoading] = useState(false);
      
    //     useEffect(() => {
    //       if (isLoading) {
    //         simulateNetworkRequest().then(() => {
    //           setLoading(false);
    //         });
    //       }
    //     }, [isLoading]);
      
    //     const handleClick = () => setLoading(true);
      
        return (
          <Button
            variant="primary"
            // disabled={isLoading}
            // onClick={!isLoading ? handleClick : null}
          >
              {text}
            {/* {isLoading ? 'Loading…' : 'Click to load'} */}
          </Button>
        );
      }
export default LoadingButton;